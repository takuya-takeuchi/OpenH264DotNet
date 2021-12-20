using System.Drawing.Imaging;
using System.Net;

using NLog;
using RtspClientSharp;
using RtspClientSharp.RawFrames.Video;
using RtspClientSharp.Rtsp;

using OpenH264DotNet;

namespace RtspDecoder
{

    internal class Program
    {

        #region Fields

        private static Logger Logger = LogManager.GetCurrentClassLogger();

        private static SVCDecoder Decoder;

        private static DecodingParam DecodingParam;

        #endregion

        #region Methods

        private static void Main(string[] args)
        {
            // It is necessary to input file contains japanese characters.
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);


            if (args.Length != 3)
            {
                Logger.Fatal($"[{nameof(Main)}] {nameof(RtspDecoder)} <url> <user> <password>>");
                return;
            }

            var url = args[0];
            var user = args[1];
            var password = args[2];

            var cancellationTokenSource = new CancellationTokenSource();

            var serverUri = new Uri(url);
            var credentials = new NetworkCredential(user, password);
            var connectionParameters = new ConnectionParameters(serverUri, credentials);
            connectionParameters.RtpTransport = RtpTransportProtocol.TCP;

            var connectTask = ConnectAsync(connectionParameters, cancellationTokenSource.Token);

            Console.WriteLine("Press any key to cancel");
            Console.ReadLine();

            cancellationTokenSource.Cancel();

            Console.WriteLine("Canceling");
            connectTask.Wait(CancellationToken.None);
        }

        #region Helpers

        private static async Task ConnectAsync(ConnectionParameters connectionParameters, CancellationToken token)
        {
            try
            {
                TimeSpan delay = TimeSpan.FromSeconds(5);

                byte[] lastIFrame = null;
                int count = 0;

                using var rtspClient = new RtspClient(connectionParameters);
                rtspClient.FrameReceived +=
                    (sender, frame) =>
                    {
                        switch (frame)
                        {
                            case RawH264IFrame h264IFrame:
                                {
                                    Console.WriteLine($"New FrameSegment frame {h264IFrame.Timestamp}: {h264IFrame.GetType().Name}, {h264IFrame.FrameSegment.Count} bytes, {h264IFrame.FrameSegment.Offset} offset");
                                    Console.WriteLine($"New SpsPpsSegment frame {h264IFrame.Timestamp}: {h264IFrame.GetType().Name}, {h264IFrame.SpsPpsSegment.Count} bytes, {h264IFrame.SpsPpsSegment.Offset} offset");

                                    //var buffer = new byte[h264IFrame.FrameSegment.Count];
                                    //Array.Copy(h264IFrame.FrameSegment.Array, h264IFrame.FrameSegment.Offset, buffer, 0, buffer.Length);
                                    //using var info = new BufferInfo();
                                    //var ret = Decoder.DecodeFrame2(buffer, info);

                                    var buffer = new byte[h264IFrame.FrameSegment.Count + h264IFrame.SpsPpsSegment.Count];
                                    Array.Copy(h264IFrame.SpsPpsSegment.Array, h264IFrame.SpsPpsSegment.Offset, buffer, 0, h264IFrame.SpsPpsSegment.Count);
                                    Array.Copy(h264IFrame.FrameSegment.Array, h264IFrame.FrameSegment.Offset, buffer, h264IFrame.SpsPpsSegment.Count, h264IFrame.FrameSegment.Count);
                                    using var info = new BufferInfo();
                                    var ret = Decoder.DecodeFrame2(buffer, info);
                                    if (ret != DecodingState.ErrorFree)
                                        break;
                                    if (info.BufferStatus != 1)
                                        break;

                                    using var bitmap = info.ToBitmap();
                                    bitmap.Save($"f:\\{count:D10}.jpg", ImageFormat.Jpeg);
                                    count++;

                                    lastIFrame = buffer;

                                    break;
                                }
                            case RawH264PFrame h264PFrame:
                                Console.WriteLine($"New FrameSegment frame {h264PFrame.Timestamp}: {h264PFrame.GetType().Name}, {h264PFrame.FrameSegment.Count} bytes, {h264PFrame.FrameSegment.Offset} offset");
                                if (lastIFrame != null)
                                {
                                    var buffer = new byte[h264PFrame.FrameSegment.Count + lastIFrame.Length];
                                    Array.Copy(lastIFrame, 0, buffer, 0, lastIFrame.Length);
                                    Array.Copy(h264PFrame.FrameSegment.Array, h264PFrame.FrameSegment.Offset, buffer, lastIFrame.Length, h264PFrame.FrameSegment.Count);
                                    using var info = new BufferInfo();
                                    var ret = Decoder.DecodeFrame2(buffer, info);
                                    if (ret != DecodingState.ErrorFree)
                                        break;
                                    if (info.BufferStatus != 1)
                                        break;

                                    using var bitmap = info.ToBitmap();
                                    bitmap.Save($"f:\\{count:D10}.jpg", ImageFormat.Jpeg);
                                    count++;
                                }
                                break;
                        }
                    };


                while (true)
                {
                    Console.WriteLine("Connecting...");

                    try
                    {
                        await rtspClient.ConnectAsync(token);
                    }
                    catch (OperationCanceledException)
                    {
                        return;
                    }
                    catch (RtspClientException e)
                    {
                        Console.WriteLine(e.ToString());
                        await Task.Delay(delay, token);
                        continue;
                    }

                    Console.WriteLine("Connected.");

                    try
                    {
                        // setup h264 decoder
                        Decoder = OpenH264.WelsCreateDecoder();

                        DecodingParam = new DecodingParam
                        {
                            CpuLoad = 0,
                            EcActiveIdc = ErrorConIdc.SliceCopy,
                            ParseOnly = false,
                            TargetDqLayer = uint.MaxValue
                        };
                        DecodingParam.VideoProperty.VideoBsType = VideoBitstreamType.Default;

                        var ret = Decoder.Initialize(DecodingParam);
                    }
                    catch (Exception)
                    {
                        Decoder.Dispose();
                        DecodingParam.Dispose();

                        return;
                    }

                    try
                    {
                        await rtspClient.ReceiveAsync(token);
                    }
                    catch (OperationCanceledException)
                    {
                        Decoder.Dispose();
                        DecodingParam.Dispose();

                        return;
                    }
                    catch (RtspClientException e)
                    {
                        Console.WriteLine(e.ToString());
                        await Task.Delay(delay, token);
                    }
                }

            }
            catch (OperationCanceledException)
            {
            }
        }

        #endregion

        #endregion

    }

}
