using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AndroidX.AppCompat.App;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace market_miniproject
{
    [Activity(Label = "SettingsPageActivity")]
    public class SettingsPageActivity : Activity
    {
        private Switch _themeSwitch;

        private Button _cancel_settingsBtn, _save_settingsBtn, _saveAndExit_settingsBtn;

        const string PREFS_NAME = "ThemePrefs";
        const string KEY_THEME = "AppTheme";
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.settings_page);
            Init();
        }
        public enum AppTheme
        {
            Light,
            Dark
        }
        void Init()
        {
            _themeSwitch = FindViewById<Switch>(Resource.Id.themeSwitch);
            _cancel_settingsBtn = FindViewById<Button>(Resource.Id.cancel_settingsBtn);
            _save_settingsBtn = FindViewById<Button>(Resource.Id.save_settingsBtn);
            _saveAndExit_settingsBtn = FindViewById<Button>(Resource.Id.saveAndExit_settingsBtn);

            _cancel_settingsBtn.Click += _cancel_settingsBtn_Click;
            //_saveAndExit_settingsBtn.Click += _saveAndExit_settingsBtn_Click;
            //_save_settingsBtn.Click += _save_settingsBtn_Click;


            //---------------------------------------------------------------------------------------

            LoadThemePreference();

            _themeSwitch.CheckedChange += (s, e) =>
            {
                if (e.IsChecked)
                {
                    SetAppTheme(AppTheme.Dark);
                }
                else
                {
                    SetAppTheme(AppTheme.Light);
                }
            };

        }

        private void _save_settingsBtn_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void _saveAndExit_settingsBtn_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void _cancel_settingsBtn_Click(object sender, EventArgs e)
        {
            Finish();
        }
        void SetAppTheme(AppTheme theme)
        {
            var prefs = GetSharedPreferences(PREFS_NAME, FileCreationMode.Private);
            var editor = prefs.Edit();

            if (theme == AppTheme.Dark)
            {
                AppCompatDelegate.DefaultNightMode = AppCompatDelegate.ModeNightYes;
                editor.PutInt(KEY_THEME, (int)AppTheme.Dark);
            }
            else
            {
                AppCompatDelegate.DefaultNightMode = AppCompatDelegate.ModeNightNo;
                editor.PutInt(KEY_THEME, (int)AppTheme.Light);
            }

            editor.Apply();

            // Restart activity to apply theme
            Recreate();
        }
        void LoadThemePreference()
        {
            var prefs = GetSharedPreferences(PREFS_NAME, FileCreationMode.Private);
            int savedTheme = prefs.GetInt(KEY_THEME, (int)AppTheme.Light);

            if (savedTheme == (int)AppTheme.Dark)
            {
                AppCompatDelegate.DefaultNightMode = AppCompatDelegate.ModeNightYes;
                _themeSwitch.Checked = true; // Set switch to "ON"
            }
            else
            {
                AppCompatDelegate.DefaultNightMode = AppCompatDelegate.ModeNightNo;
                _themeSwitch.Checked = false; // Set switch to "OFF"
            }
        }
    }
}