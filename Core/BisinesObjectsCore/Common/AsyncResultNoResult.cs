using System;
using System.Threading;
namespace BusinessObjects
{
    internal class AsyncResultNoResult: IAsyncResult
    {
        private readonly AsyncCallback _mAsyncCallback;
        private readonly object _mAsyncState;

        private const int _StatePending=0;
        private const int c_StateComplitedSynchronously = 1;
        private const int c_StateComplitedAsynchronously = 2;
        private int _mComplitedState = _StatePending;

        private ManualResetEvent _mAsyncWaitHandle;
        private Exception _mExeption;

        public AsyncResultNoResult(AsyncCallback asyncCallback, object state)
        {
            _mAsyncCallback = asyncCallback;
            _mAsyncState = state;
        }

        public void SetAsComplited(Exception exception, bool complitedSynchronously)
        {
            _mExeption = exception;
            int prevState = Interlocked.Exchange(ref _mComplitedState, complitedSynchronously ?
            c_StateComplitedSynchronously : c_StateComplitedAsynchronously);
            if (prevState != _StatePending)
                throw new InvalidOperationException("Вы можете установить результат только раз");

            if (_mAsyncWaitHandle != null)
                _mAsyncWaitHandle.Set();

            if (_mAsyncCallback != null)
                _mAsyncCallback(this);
        }
        public void EndInvoke()
        {
            if (!IsCompleted)
            {
                AsyncWaitHandle.WaitOne();
                AsyncWaitHandle.Close();
                _mAsyncWaitHandle = null;
            }

            if (_mExeption != null)
                throw _mExeption;
        }
        #region IAsyncResult Members
        public object AsyncState
        {
            get { return _mAsyncState; }
        }

        public WaitHandle AsyncWaitHandle
        {
            get 
            {
                if (_mAsyncWaitHandle == null)
                {
                    bool done = IsCompleted;
                    ManualResetEvent mre = new ManualResetEvent(done);
                    if (Interlocked.CompareExchange(ref _mAsyncWaitHandle, mre, null) != null)
                    {
                        mre.Close();
                    }
                    else
                    {
                        if (!done && IsCompleted)
                        {
                            _mAsyncWaitHandle.Set();
                        }
                    }
                }
                return _mAsyncWaitHandle;
            }
        }

        public bool CompletedSynchronously
        {
            get { return Thread.VolatileRead(ref _mComplitedState) == c_StateComplitedSynchronously; }
        }

        public bool IsCompleted
        {
            get { return Thread.VolatileRead(ref _mComplitedState)!= _StatePending; }
        }

        #endregion
    }
}
