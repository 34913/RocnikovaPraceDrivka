using System;
using System.Collections.Generic;
using System.Text;

using Xamarin.Forms;

using RocnikovaPraceDrivka.Controls;

namespace RocnikovaPraceDrivka.MyElements
{
	public class MyClassButton: Button
	{

        public Class ClassItem
        {
            get
            {
                return (Class)GetValue(ClassItemProperty);
            }
            set
            {
                SetValue(ClassItemProperty, value);
            }
        }

        public static readonly BindableProperty ClassItemProperty = BindableProperty.Create(nameof(ClassItem), typeof(Class), typeof(MyClassButton));

        //

        public MyClassButton()
            : base()
		{

		}

	}
}
