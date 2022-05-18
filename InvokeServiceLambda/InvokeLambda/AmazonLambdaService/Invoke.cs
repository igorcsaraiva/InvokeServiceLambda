using Amazon;
using Amazon.Lambda;
using Amazon.Lambda.Model;
using Amazon.SecurityToken.Model;
using InvokeLambda.Domain;
using InvokeLambda.Interfaces;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace InvokeLambda.AmazonLambdaService
{
    class Invoke : IInvoke
    {
        private readonly IAmazonLambda _amazonLambda;

        public Invoke(RegionEndpoint regionEndpoint)
        {
            _amazonLambda = new AmazonLambdaClient(regionEndpoint);
        }
        public Invoke(Credentials credentials, RegionEndpoint regionEndpoint)
        {
            _amazonLambda = new AmazonLambdaClient(credentials, regionEndpoint);
        }

        /// <summary>
        /// Invokes a Lambda function.
        /// </summary>
        /// <param name="request">Container for the parameters to the Invoke operation. Invokes a Lambda function.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>The response from the Invoke service method, as returned by Lambda.</returns>
        /// <exception cref="EC2AccessDeniedException">Need additional permissions to configure VPC settings.</exception>
        /// <exception cref="EC2ThrottledException">Lambda was throttled by Amazon EC2 during Lambda function initialization using the execution role provided for the Lambda function.</exception>
        /// <exception cref="EC2UnexpectedException">Lambda received an unexpected EC2 client exception while setting up for the Lambda function.</exception>
        /// <exception cref="EFSIOException">An error occurred when reading from or writing to a connected file system.</exception>
        /// <exception cref="EFSMountConnectivityException">The function couldn't make a network connection to the configured file system.</exception>
        /// <exception cref="EFSMountFailureException">The function couldn't mount the configured file system due to a permission or configuration issue.</exception>
        /// <exception cref="EFSMountTimeoutException">The function was able to make a network connection to the configured file system, but the mount operation timed out.</exception>
        /// <exception cref="ENILimitReachedException">Lambda was not able to create an elastic network interface in the VPC, specified as part of Lambda function configuration, because the limit for network interfaces has been reached.</exception>
        /// <exception cref="InvalidParameterValueException">One of the parameters in the request is invalid.</exception>
        /// <exception cref="InvalidRequestContentException">The request body could not be parsed as JSON.</exception>
        /// <exception cref="InvalidRuntimeException">The runtime or runtime version specified is not supported.</exception>
        /// <exception cref="InvalidSecurityGroupIDException">The Security Group ID provided in the Lambda function VPC configuration is invalid.</exception>
        /// <exception cref="InvalidSubnetIDException">The Subnet ID provided in the Lambda function VPC configuration is invalid.</exception>
        /// <exception cref="KMSAccessDeniedException">Lambda was unable to decrypt the environment variables because KMS access was denied. Check the Lambda function's KMS permissions.</exception>
        /// <exception cref="KMSDisabledException">Lambda was unable to decrypt the environment variables because the KMS key used is disabled. Check the Lambda function's KMS key settings.</exception>
        /// <exception cref="KMSInvalidStateException">Lambda was unable to decrypt the environment variables because the KMS key used is in an invalid state for Decrypt. Check the function's KMS key settings.</exception>
        /// <exception cref="KMSNotFoundException">Lambda was unable to decrypt the environment variables because the KMS key was not found. Check the function's KMS key settings.</exception>
        /// <exception cref="RequestTooLargeException">The request payload exceeded the Invoke request body JSON input limit. For more information, see Limits.</exception>
        /// <exception cref="ResourceConflictException">The resource already exists, or another operation is in progress.</exception>
        /// <exception cref="ResourceNotFoundException">The resource specified in the request does not exist.</exception>
        /// <exception cref="ResourceNotReadyException">The function is inactive and its VPC connection is no longer available. Wait for the VPC connection to reestablish and try again.</exception>
        /// <exception cref="ServiceException">The Lambda service encountered an internal error.</exception>
        /// <exception cref="SubnetIPAddressLimitReachedException">Lambda was not able to set up VPC access for the Lambda function because one or more configured subnets has no available IP addresses.</exception>
        /// <exception cref="TooManyRequestsException">The request throughput limit was exceeded.</exception>
        /// <exception cref="UnsupportedMediaTypeException">The content type of the Invoke request body is not JSON.</exception>
        public async Task<T> InvokeLambdaAsync<T, K>(K request, CancellationToken cancellationToken = default) where T : Response, new() where K : Request, new()
        {
            var responseBody = await _amazonLambda.InvokeAsync(new InvokeRequest { FunctionName = request.FunctionName , Payload = request.Payload, InvocationType = request.InvocationType}, cancellationToken);

            return new T { Payload = ConvertMemoryStreamToString(responseBody.Payload), StatusCode = responseBody.StatusCode };
        }

        private string ConvertMemoryStreamToString(MemoryStream memoryStream)
        {
            return Encoding.UTF8.GetString(memoryStream.ToArray());
        }
    }
}
