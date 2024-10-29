using System;
using CommunityToolkit.Mvvm.Messaging.Messages;

namespace FilmFinderTMDB.Source.Message
{
    public class ScanCodeMessage : ValueChangedMessage<object>
    {
        public ScanCodeMessage(object value) : base(value)
        {
        }
    }
}

