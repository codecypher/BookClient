using System;
using Android.Content;
using Android.Util;
using Android.Views;
using Android.Widget;
using Java.Lang;
using Java.Lang.Reflect;
using Java.Util;
using Android.OS;
using Android.Graphics;


namespace BookClient.Droid
{
    // XamarinFormsToolbarCustomFont
    // https://github.com/daniel-luberda/XamarinFormsToolbarCustomFont
    // Based on http://stackoverflow.com/a/5205945/5064986
    public class CustomLayoutInflaterFactory : Java.Lang.Object, LayoutInflater.IFactory
	{
		static Class ActionMenuItemViewClass = null;
		static Constructor ActionMenuItemViewConstructor = null;

        private static string[] fonts = { "fa-brands-400.ttf", "fa-regular-400.ttf", "fa-solid-900.ttf" };
        private static HashMap fontCache = new HashMap();

        static Typeface typeface = null;

        public static Typeface Typeface
		{
			get
			{
				if (typeface == null) 
					typeface = Typeface.CreateFromAsset(Xamarin.Forms.Forms.Context.ApplicationContext.Assets, "Fonts/fa-solid-900.ttf");
				
				return typeface;
			}
		}

        public static Typeface getTypeface(int fontPos, Context context)
        {
            string font = "Fonts/" + fonts[fontPos];
            Typeface typeface = (Typeface)fontCache.Get(font);

            if (typeface == null)
            {
                typeface = Typeface.CreateFromAsset(context.Assets, font);
                fontCache.Put(font, typeface);
            }

            return typeface;
        }

        public View OnCreateView(string name, Context context, IAttributeSet attrs)
		{
			if (name.Equals("com.android.internal.view.menu.ActionMenuItemView", StringComparison.InvariantCultureIgnoreCase))
			{
				View view = null;

				try
				{
					if (ActionMenuItemViewClass == null)
						ActionMenuItemViewClass = ClassLoader.SystemClassLoader.LoadClass(name);
				}
				catch (ClassNotFoundException)
				{
					return null;
				}

				if (ActionMenuItemViewClass == null)
					return null;

				if (ActionMenuItemViewConstructor == null)
				{
					try
					{
						ActionMenuItemViewConstructor = ActionMenuItemViewClass.GetConstructor(new Class[] {
							Class.FromType(typeof(Context)),
							Class.FromType(typeof(IAttributeSet))
						});
					}
					catch (SecurityException)
					{
						return null;
					}
					catch (NoSuchMethodException)
					{
						return null;
					}
				}
				if (ActionMenuItemViewConstructor == null)
					return null;

				try
				{
					Java.Lang.Object[] args = new Java.Lang.Object[] { context, (Java.Lang.Object)attrs };
					view = (View)(ActionMenuItemViewConstructor.NewInstance(args));
				}
				catch (IllegalArgumentException)
				{
					return null;
				}
				catch (InstantiationException)
				{
					return null;
				}
				catch (IllegalAccessException)
				{
					return null;
				}
				catch (InvocationTargetException)
				{
					return null;
				}
				if (null == view)
					return null;

				View v = view;

				new Handler().Post(() => {

					try
					{
                        if(v is LinearLayout) {
                            var ll = (LinearLayout)v;
                            for(int i = 0; i < ll.ChildCount; i++) {
                                var button = ll.GetChildAt(i) as Button;

                                if(button != null) {
                                    var title = button.Text;

                                    if (!string.IsNullOrEmpty(title) && title.Length == 1)
                                    {
                                        for (int n=0; n < fonts.Length; n++)
                                        {
                                            button.SetTypeface(getTypeface(n, context), TypefaceStyle.Normal);
                                            button.SetTextSize(ComplexUnitType.Sp, 24);
                                        }
                                    }
                                }
                            }
                        }
                        else if(v is TextView) {
                            var tv = (TextView)v;
                            string title = tv.Text;

                            if (!string.IsNullOrEmpty(title) && title.Length == 1)
                            {
                                for (int n = 0; n < fonts.Length; n++)
                                {
                                    tv.SetTypeface(getTypeface(n, context), TypefaceStyle.Normal);
                                    tv.SetTextSize(ComplexUnitType.Sp, 24);
                                }
                            }
                        }
					}
					catch (ClassCastException)
					{
					}
				});

				return view;
			}

			return null;
		}
	}
}

