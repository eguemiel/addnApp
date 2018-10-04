using Android.Content;
using Android.Runtime;
using Android.Util;
using TiagoSantos.EnchantedViewPager;
using static TiagoSantos.EnchantedViewPager.EnchantedViewPager;

namespace AddnApp.Base
{
    [Register("addnapp.base.EnchantedViewPagerExtended")]
    public class EnchantedViewPagerExtended : EnchantedViewPager, IEnchantedViewPagerSwipeListener
    {
        public EnchantedViewPagerExtended(Context context) : base(context)
        {
        }

        public EnchantedViewPagerExtended(Context context, IAttributeSet attrs) : base(context, attrs)
        {
        }

        
    }
}