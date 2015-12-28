using System;

namespace BusinessObjects
{
    internal class AsyncResult<TResult> : AsyncResultNoResult
    {
        private TResult _mResult = default(TResult);

        public AsyncResult(AsyncCallback asyncCallback, object state)
            : base(asyncCallback, state)
        {
        }
        public void SetAsComplited(TResult result, bool complitedSynchronously)
        {
            _mResult = result;
            SetAsComplited(null, complitedSynchronously);
        }
        new public TResult EndInvoke()
        {
            base.EndInvoke();
            return _mResult;
        }
    }
}