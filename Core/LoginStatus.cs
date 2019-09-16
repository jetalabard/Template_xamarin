namespace Core
{
    public enum LoginStatus
    {
        /// <summary>
        /// success status.
        /// </summary>
        Success,

        /// <summary>
        /// server error status.
        /// </summary>
        ServerError,

        /// <summary>
        /// invalid request status.
        /// </summary>
        BadRequest,

        /// <summary>
        /// invalid credentials status.
        /// </summary>
        InvalidCredential,

        /// <summary>
        /// no internet status.
        /// </summary>
        Offline,
    }
}