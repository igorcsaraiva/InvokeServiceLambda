namespace InvokeLambda.Domain
{
    public abstract class Response
    {
        /// <summary>
        /// Gets and sets the property Payload.
        /// The response from the function, or an error object.
        /// </summary>
        public string Payload { get; set; }
        /// <summary>
        /// Gets and sets the property StatusCode.
        /// The HTTP status code is in the 200 range for a successful request. 
        /// For the RequestResponse invocation type, this status code is 200.
        /// For the Event invocation type, this status code is 202.
        /// For the DryRun invocation type, the status code is 204.
        /// </summary>
        public int StatusCode { get; set; }
    }
}
