namespace Application
{
    class ItemMapp
    {
        public long InValue { get; set; }
        public long InValueFrom
        {
            get
            {
                return InValue;
            }
        }
        public long InValueTo
        {
            get
            {
                return InValueFrom + Amount - 1;
            }
        }
        public long OutValue { get; set; }
        public long OutValueFrom
        {
            get
            {
                return OutValue;
            }
        }
        public long OutValueTo
        {
            get
            {
                return OutValue + Amount - 1;
            }
        }
        public long Amount { get; set; }
        public long Offset { get; set; }
        public ItemMapp(string data)
        {
            string[] _dataParts = data.Split(" ".ToCharArray(), StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

            InValue = long.Parse(_dataParts[1]);
            OutValue = long.Parse(_dataParts[0]);
            Offset = (long)InValue - OutValue;
            Amount = long.Parse(_dataParts[2]);
        }

        internal long GetPointer(long value)
        {
            return OutValue + (value - InValue);
        }
    }
}