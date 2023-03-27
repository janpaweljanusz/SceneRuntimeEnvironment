const onStart = originalRequire('onStart');
const onUpdate = originalRequire('onUpdate');

/**
 * wait in miliseconds
 * @param {number} msec - miliseconds to wait 
 */
const waitAsync = async (msec) => {
    await Task.Delay(msec).ToPromise();
}
/**
 * @access private
 * @param {*} func 
 * @param {*} done 
 * @param {*} dt 
 * @param {*} waitMiliseconds 
 */
const AsyncWrapper = async (func, done, dt, waitMiliseconds)  =>{
    if (waitMiliseconds > 0){
        await waitAsync(1000);
    }
    await func(dt);
    done.Set();
}
/**
 * Wrapper on JavaScript sandboxed file onStart for allowing asynchronous from C# context
 * @param {object} done - an instance of ManualResetEventSlim
 * @param {string | number | undefined | null} dt - variable passed from 
 */
globalThis.onStartWrapper = async (done, dt) => {
    AsyncWrapper(onStart, done, dt)
}
/**
 * Wrapper on JavaScript sandboxed file onUpdate for allowing asynchronous from C# context
 * @param {object} done - an instance of ManualResetEventSlim
 * @param {string | number | undefined | null} dt - variable passed from 
 */
globalThis.onUpdateWrapper = async (done, dt) => {
    AsyncWrapper(onUpdate, done, dt, 1000)
}