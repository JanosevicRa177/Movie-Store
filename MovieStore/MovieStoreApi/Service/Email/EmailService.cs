using System.Net;
using System.Net.Mail;
using Microsoft.Extensions.Options;
using MimeKit;
using MovieStore.Core.Model;
using MovieStoreApi.Repositories.Interfaces;

namespace MovieStoreApi.Service.Email;

public class EmailService: IEmailService
{
    private readonly IRepository<PurchasedMovie> _purchasedMovieRepository;
    private readonly EmailOptions _emailOptions;
    

    public EmailService(IRepository<PurchasedMovie> purchasedMovieRepository, EmailOptions emailOptions)
    {
        _purchasedMovieRepository = purchasedMovieRepository;
        _emailOptions = emailOptions;
    }

    public void SendExpirationEmails()
    {
        var expiredMovies =  _purchasedMovieRepository.Search(movie => 
            movie.ExpirationDate.Date < DateTime.Now && movie.ExpirationDate.Date > DateTime.Now.AddMinutes(-5));

        foreach (var expiredMovie in expiredMovies) SendExpiredMovieEmail(expiredMovie);
    }

    private void SendExpiredMovieEmail(PurchasedMovie purchasedMovie)
    {
        var emailBody = @"<div style=""border-bottom: 24px solid #484444; border-top: 24px solid #484444;"">
                    <h1 style=""background: aquamarine; padding: 16px 8px; margin: 0;"">Email for expiration</h1>
                    <p>Your movie: " + purchasedMovie.Movie.Name + " expired today</p></div>";
        var subject = "Movie expiration notification";
        SendEmail(purchasedMovie.Customer.Email.Value, emailBody, subject);
    }

    private void SendEmail(string customerEmail,string body,string subject)
    {
        var newMail = new MailMessage();
        newMail.From = new MailAddress(_emailOptions.From, _emailOptions.Name);
        newMail.To.Add(customerEmail);
        newMail.IsBodyHtml = true;
        var client = new SmtpClient(_emailOptions.Host, _emailOptions.Port);
        newMail.Body = body;
        newMail.Subject = subject;
        client.EnableSsl = true;
        var credentials = new NetworkCredential(_emailOptions.From, _emailOptions.Password);
        client.Credentials = credentials;
        client.Send(newMail);
    }
}