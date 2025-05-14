using System.Threading;
using System.Threading.Tasks;
using Google;
using UnityEngine;

internal interface IGoogleSignInManager
{
    Task<GoogleSignInUser> SignInWithGoogleSilentlyAsync(CancellationToken token = default);
    Task<GoogleSignInUser> SignInWithGoogleAsync(CancellationToken token = default);
}

internal class AndroidGoogleSignInManager : IGoogleSignInManager
{
    private const string WEB_CLIENT_ID = "629535612254-micnl1rvk94bpf8ijoldeuvf8mtqevkk.apps.googleusercontent.com";
    private readonly Google.GoogleSignInConfiguration configuration;

    public AndroidGoogleSignInManager()
    {
        configuration = new Google.GoogleSignInConfiguration
        {
            WebClientId = WEB_CLIENT_ID,
            RequestIdToken = true,
            RequestEmail = true,
            RequestAuthCode = true
        };
    }

    public async Task<GoogleSignInUser> SignInWithGoogleSilentlyAsync(CancellationToken token = default)
    {
        try
        {
            Google.GoogleSignIn.Configuration = configuration;
            var user = await Google.GoogleSignIn.DefaultInstance.SignInSilently();
            if (token.IsCancellationRequested)
            {
                return null;
            }

            if (user == null)
            {
                Debug.LogError("[GoogleAuth] Sign-In failed: user is null");
                return null;
            }

            return new GoogleSignInUser
            {
                AuthCode = user.AuthCode,
                DisplayName = user.DisplayName,
                Email = user.Email,
                IdToken = user.IdToken,
                UserId = user.UserId,
            };
        }
        catch (System.Exception e)
        {
            Debug.LogError("[GoogleAuth] Sign-In Failed: " + e.Message);
            return null;
        }
    }

    public async Task<GoogleSignInUser> SignInWithGoogleAsync(CancellationToken token = default)
    {
        try
        {
            Google.GoogleSignIn.Configuration = configuration;
            var user = await Google.GoogleSignIn.DefaultInstance.SignIn();
            if (token.IsCancellationRequested)
            {
                return null;
            }

            if (user == null)
            {
                Debug.LogError("[GoogleAuth] Sign-In failed: user is null");
                return null;
            }

            return new GoogleSignInUser
            {
                AuthCode = user.AuthCode,
                DisplayName = user.DisplayName,
                Email = user.Email,
                IdToken = user.IdToken,
                UserId = user.UserId,
            };
        }
        catch (System.Exception e)
        {
            Debug.LogError("[GoogleAuth] Sign-In Failed: " + e.Message);
            return null;
        }
    }
}