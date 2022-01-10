namespace ProSwapperLobby.Data
{
    public class SkinObj
    {
        public class Type
        {
            public string backendValue { get; set; }
        }

        public class Rarity
        {
            public string value { get; set; }
            public string bgcss { get; set; }
        }

        public class Introduction
        {
            public int backendValue { get; set; }
        }

        public class Images
        {
            public string smallIcon { get; set; }
            public string icon { get; set; }
        }

        public class Option
        {
            public string tag { get; set; }
            public string name { get; set; }
            public string image { get; set; }
        }

        public class Variant
        {
            public string channel { get; set; }
            public string type { get; set; }
            public List<Option> options { get; set; }
        }

        public class Datum
        {
            public string id { get; set; }
            public string name { get; set; }
            public string description { get; set; }
            public Type type { get; set; }
            public Rarity rarity { get; set; }
            public Introduction introduction { get; set; }
            public Images images { get; set; }
            public List<Variant> variants { get; set; }
        }

        public class Root
        {
            public int status { get; set; }
            public Datum[] data { get; set; }
        }


    }
}
