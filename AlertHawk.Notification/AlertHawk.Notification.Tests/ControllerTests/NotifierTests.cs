using AlertHawk.Notification.Controllers;
using AlertHawk.Notification.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AlertHawk.Notification.Tests.ControllerTests;

public class NotifierTests : IClassFixture<NotificationController>
{
    private readonly NotificationController _notificationController;

    public NotifierTests(NotificationController notificationController)
    {
        _notificationController = notificationController;
    }

    [Fact]
    public async Task Should_Send_Telegram_Notification()
    {
        var notificationSend = new NotificationSend
        {
            Message = "Message",
            NotificationTypeId = 3, // Telegram
            NotificationTelegram = new NotificationTelegram
            {
                ChatId = GlobalVariables.TelegramChatId,
                NotificationId = 1,
                TelegramBotToken = GlobalVariables.TelegramWebHook,
            },
        };
        var result = await _notificationController.SendNotification(notificationSend) as OkObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.Equal(StatusCodes.Status200OK, result.StatusCode);
        Assert.Equal(true, result.Value);
    }

    [Fact]
    public async Task Should_Send_EmailSmtp_Notification()
    {
        var notificationSend = new NotificationSend
        {
            Message = "Message",
            NotificationTypeId = 1, // EmailSmtp
            NotificationEmail = new NotificationEmail
            {
                ToEmail = "alerthawk@outlook.com",
                FromEmail = "alerthawk@outlook.com",
                Username = "alerthawk@outlook.com",
                Password = GlobalVariables.EmailPassword,
                Hostname = "smtp.office365.com",
                Body = "Body",
                Subject = "Subject",
                Port = 587,
                EnableSsl = true,
                IsHtmlBody = false,
                NotificationId = 1
            },
        };
        var result = await _notificationController.SendNotification(notificationSend) as OkObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.Equal(StatusCodes.Status200OK, result.StatusCode);
        Assert.Equal(true, result.Value);
    }
}