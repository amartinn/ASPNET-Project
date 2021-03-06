﻿namespace CasesNET.Data.Common
{
    public class Validation
    {
        public class Checkout
        {
            public const int FirstNameMaxLength = 10;

            public const int LastNameMaxLength = 10;

            public const int AdressMaxLength = 20;
        }

        public class Case
        {
            public const int NameMaxLength = 30;

            public const double MinPrice = 2D;

            public const double MaxPrice = 100D;

            public const int MaxDescriptionLength = 200;
        }

        public class Category
        {
            public const int MaxNameLength = 30;
        }

        public class Manufacturer
        {
            public const int MaxNameLength = 30;
        }

        public class Device
        {
            public const int MaxNameLength = 30;
        }
    }
}
