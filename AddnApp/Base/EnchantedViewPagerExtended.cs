using Android.Content;
using Android.Runtime;
using Android.Support.V4.View;
using Android.Util;
using Android.Views;
using TiagoSantos.EnchantedViewPager;
using static TiagoSantos.EnchantedViewPager.EnchantedViewPager;

namespace AddnApp.Base
{
    [Register("addnapp.base.EnchantedViewPagerExtended")]
    public class EnchantedViewPagerExtended : EnchantedViewPager
    {
        public bool SwipeToRight { get; set; }
        public bool SwipeToLeft { get; set; }

        public EnchantedViewPagerExtended(Context context) : base(context)
        {
        }

        public EnchantedViewPagerExtended(Context context, IAttributeSet attrs) : base(context, attrs)
        {
       
        }        
    }
}