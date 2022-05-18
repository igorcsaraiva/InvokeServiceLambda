using Amazon.SecurityToken.Model;
using System.Threading.Tasks;

namespace InvokeLambda.Interfaces
{
    public interface ISecurity
    {
        /// <summary>
        /// Returns a set of temporary security credentials that you can use to access Amazon Web Services resources that you might not normally have access to.
        /// </summary>
        /// <param name="roleArn">The Amazon Resource Name (ARN) of the role to assume.</param>
        /// <param name="roleSessionName">
        ///     An identifier for the assumed role session.
        ///     Use the role session name to uniquely identify a session when the same role is
        ///     assumed by different principals or for different reasons. In cross-account scenarios,
        ///     the role session name is visible to, and can be logged by the account that owns
        ///     the role. The role session name is also used in the ARN of the assumed role principal.
        ///     This means that subsequent cross-account API requests that use the temporary
        ///     security credentials will expose the role session name to the external account
        ///     in their CloudTrail logs.
        ///     The regex used to validate this parameter is a string of characters consisting
        ///     of upper- and lower-case alphanumeric characters with no spaces. You can also
        ///     include underscores or any of the following characters: =,.@-</param>
        /// <returns>
        ///  Permissions the temporary security credentials created by AssumeRole
        /// </returns>
        /// <exception cref="ExpiredTokenException">The web identity token that was passed is expired or is not valid. Get a new identity token from the identity provider and then retry the request.</exception>
        /// <exception cref="MalformedPolicyDocumentException">The request was rejected because the policy document was malformed. The error message describes the specific error.</exception>
        /// <exception cref="PackedPolicyTooLargeException">
        ///     The request was rejected because the total packed size of the session policies
        ///     and session tags combined was too large. An Amazon Web Services conversion compresses
        ///     the session policy document, session policy ARNs, and session tags into a packed
        ///     binary format that has a separate limit. The error message indicates by percentage
        ///     how close the policies and tags are to the upper size limit. For more information,
        ///     see Passing Session Tags in STS in the IAM User Guide.
        ///     You could receive this error even though you meet other defined session policy
        ///     and session tag limits. For more information, see IAM and STS Entity Character
        ///     Limits in the IAM User Guide.</exception>
        /// <exception cref="RegionDisabledException">
        ///     STS is not activated in the requested region for the account that is being asked
        ///     to generate credentials. The account administrator must use the IAM console to
        ///     activate STS in that region. For more information, see Activating and Deactivating
        ///     Amazon Web Services STS in an Amazon Web Services Region in the IAM User Guide.</exception>
        Task<Credentials> AssumeRoleAsync(string roleArn, string roleSessionName);
    }
}
