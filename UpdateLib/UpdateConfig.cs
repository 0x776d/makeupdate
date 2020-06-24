namespace UpdateLib
{
    public class UpdateConfig
    {
        public string MarshalerPath { get; set; }
        public string ModelPath { get; set; }
        public string Schema { get; set; }
        public string Model { get; set; }

        public bool SkipBeforeUpdate { get; set; }
        public bool SkipUpdate { get; set; }
        public bool SkipAfterUpdate { get; set; }
    }
}
