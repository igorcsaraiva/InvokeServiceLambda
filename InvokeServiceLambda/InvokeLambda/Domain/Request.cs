using Newtonsoft.Json;
using System.ComponentModel;

namespace InvokeLambda.Domain
{
    public class Request<T>
    {
 
        /// <summary>
        /// Sets the property Payload.
        /// Class that you want to provide to your cloud function as input.
        /// </summary>
        public T PayloadClass { private get; set; }
        /// <summary>
        /// Get Payload property in json format.
        /// </summary>
        public string Payload => ClassToJson(PayloadClass);
        /// <summary>
        /// Gets and sets the property name of the function to be invoked.
        /// </summary>
        public string FunctionName { get; set; }
        /// <summary>
        /// Gets and sets the property InvocationType.
        ///     Choose from the following options.
        ///     RequestResponse
        ///     (default) - Invoke the function synchronously. Keep the connection open until
        ///     the function returns a response or times out. The API response includes the function
        ///     response and additional data.
        ///     Event
        ///     - Invoke the function asynchronously. Send events that fail multiple times to
        ///     the function's dead-letter queue (if it's configured). The API response only
        ///     includes a status code.
        ///     DryRun
        ///     - Validate parameter values and verify that the user or role has permission to
        ///     invoke the function.   
        /// </summary>
        [DefaultValue("RequestResponse")]
        public string InvocationType { get; set; }

        private string ClassToJson(T obj)
        {
            return JsonConvert.SerializeObject(obj);
        }
    }
}
