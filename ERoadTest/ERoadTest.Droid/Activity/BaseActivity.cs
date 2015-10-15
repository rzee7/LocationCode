using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Toolbar = Android.Support.V7.Widget.Toolbar;
using Android.Support.V7.App;
using Android.Preferences;
using System.Threading.Tasks;

namespace ERoadTest.Droid
{
    public abstract class BaseActivity : AppCompatActivity
    {
        private ImageButton btnLogout;

        /// <summary>
        /// The count timer for updating the Refresh Offers & UI
        /// </summary>
        public  System.Timers.Timer countTimer;

        #region virtual Property & Methods

        public virtual int LayoutResource { get; set; }

        /// <summary>
        /// Refreshs the sync UI, called by the count timer
        /// </summary>
        public virtual void RefreshSyncUI()
        {
        }

        #endregion

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(LayoutResource);
            tToolbar = FindViewById<Toolbar>(Resource.Id.toolbar);


            SetSupportActionBar(tToolbar);
            SupportActionBar.Title = "ERoad Test";
            //Time Interval to update UI
            countTimer = new System.Timers.Timer(Constant.TimeInterval);
            countTimer.AutoReset = true;
            countTimer.Elapsed += (sender, e) =>
            {
                RefreshSyncUI();
            };
            countTimer.Start();
        }


        public Toolbar tToolbar { get; set; }

        protected override void OnDestroy()
        {
            base.OnDestroy();

            if (countTimer != null)
            {
                countTimer.Stop();
                countTimer.Dispose();
                countTimer = null;
            }
        }
    }
}