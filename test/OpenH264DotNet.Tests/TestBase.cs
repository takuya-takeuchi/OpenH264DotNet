using System;
using System.Collections.Generic;
using Xunit;

namespace OpenH264DotNet.Tests
{

    public abstract class TestBase
    {

        #region Methods

        public void DisposeAndCheckDisposedState(OpenH264Object obj)
        {
            if (obj == null)
                return;

            obj.Dispose();
            Assert.True(obj.IsDisposed);
            Assert.True(obj.NativePtr == IntPtr.Zero);
        }

        public void DisposeAndCheckDisposedStates(IEnumerable<OpenH264Object> objs)
        {
            foreach (var obj in objs)
                this.DisposeAndCheckDisposedState(obj);
        }

        #endregion

    }

}
