using System;

namespace BoardGamesManagerMvc.Models
{
    public class LastDisplayViewModel
    {
        public LastDisplayViewModel()
        {
        }

        public LastDisplayViewModel(string source, DateTimeOffset displayDatetime)
        {
            Source = source;
            DisplayDatetime = displayDatetime;
        }

        public string Source { get; set; }
        public DateTimeOffset DisplayDatetime { get; set; }

        public override string ToString() => $"Source: {Source}, DisplayDatetime: {DisplayDatetime}";
    }
}