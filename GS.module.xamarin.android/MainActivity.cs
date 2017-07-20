namespace GS.module.xamarin.android
{
    using Android;
    using Android.App;
    using Android.Graphics;
    using Android.OS;
    using Android.Webkit;
    using Android.Widget;

    [Activity(Label = "Grupo Silvestre - Mobile", MainLauncher = true, Icon = "@drawable/icon", Theme = "@android:style/Theme.Black.NoTitleBar")]
    public class MainActivity : Activity
    {
        private WebView mWebView;
        private ProgressBar mProgressBar;
        private WebClient mWebClient;
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView (Resource.Layout.Main);

            mWebClient = new WebClient();
            mWebClient.mOnProgressChanged += (int state) =>
              {
                  if (state == 0)
                  {
                      mProgressBar.Visibility = Android.Views.ViewStates.Invisible;
                  }
                  else
                  {
                      mProgressBar.Visibility = Android.Views.ViewStates.Visible;
                  }
              };

            mWebView = FindViewById<WebView>(Resource.Id.WebView);
            mProgressBar = FindViewById<ProgressBar>(Resource.Id.progressBar);

            mWebView.Settings.JavaScriptEnabled = true;
            mWebView.LoadUrl("https://intranet.gruposilvestre.com.pe/merlin/login.html");
            mWebView.SetWebViewClient(new WebViewClient());
        }
    }

    public class WebClient : WebViewClient
    {
        public delegate void ToggleProgressBar(int state);
        public ToggleProgressBar mOnProgressChanged;
        public override bool ShouldOverrideUrlLoading(WebView view, string url)
        {
            view.LoadUrl(url);
            return true;
        }

        public override void OnPageStarted(WebView view, string url, Bitmap favicon)
        {
            if (mOnProgressChanged != null)
                mOnProgressChanged.Invoke(1);
            base.OnPageStarted(view, url, favicon);
        }

        public override void OnPageFinished(WebView view, string url)
        {
            if (mOnProgressChanged != null)
                mOnProgressChanged.Invoke(0);
            base.OnPageFinished(view, url);
        }
    }
}

