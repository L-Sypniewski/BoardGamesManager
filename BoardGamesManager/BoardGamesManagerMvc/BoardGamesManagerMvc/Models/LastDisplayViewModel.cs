using System;

namespace BoardGamesManagerMvc.Models
{
    public class LastDisplayViewModel
    {
        public string Source { get; set; }
        public DateTimeOffset DisplayDatetime { get; set; }


        public LastDisplayViewModel()
        {
        }

        public LastDisplayViewModel(string source, DateTimeOffset displayDatetime)
        {
            Source = source;
            DisplayDatetime = displayDatetime;
        }

        public override string ToString() => $"Source: {Source}, DisplayDatetime: {DisplayDatetime}";
    }
}