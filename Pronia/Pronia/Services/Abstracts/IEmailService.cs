using System;
using Pronia.Contracts.Email;

namespace Pronia.Services.Abstracts
{
    public interface IEmailService
    {
        public void Send(MessageDto messageDto);
    }
}