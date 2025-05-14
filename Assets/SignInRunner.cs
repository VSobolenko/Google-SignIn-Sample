using System.Threading.Tasks;
using UnityEngine;

public class SignInRunner : MonoBehaviour
{
    // Start is called before the first frame update
    private void Start()
    { 
        StartAuth();
    }

    private static async void StartAuth()
    {
        var manager = new AndroidGoogleSignInManager();
        await manager.SignInWithGoogleAsync();
    }

    // Update is called once per frame
    void Update() { }
}