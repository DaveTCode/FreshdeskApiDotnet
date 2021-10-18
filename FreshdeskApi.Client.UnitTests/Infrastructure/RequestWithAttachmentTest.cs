using System.Collections;
using System.Collections.Generic;
using FreshdeskApi.Client.CommonModels;
using FreshdeskApi.Client.Contacts.Requests;
using FreshdeskApi.Client.Conversations.Requests;
using FreshdeskApi.Client.Tickets.Models;
using FreshdeskApi.Client.Tickets.Requests;
using Xunit;

namespace FreshdeskApi.Client.UnitTests.Infrastructure
{
    public class RequestWithAttachmentTest
    {
        [Theory]
        [ClassData(typeof(MultipartDataData))]
        public void MultipartFormDataRequiredWorks(IRequestWithAttachment request)
        {
            Assert.True(request.IsMultipartFormDataRequired());
        }

        [Theory]
        [ClassData(typeof(NoMultipartDataData))]
        public void MultipartFormDataNotRequiredWorks(IRequestWithAttachment request)
        {
            Assert.False(request.IsMultipartFormDataRequired());
        }

        public class MultipartDataData : IEnumerable<object[]>
        {
            public IEnumerator<object[]> GetEnumerator()
            {
                yield return new object[] { new ContactCreateRequest("name", avatar: new FileAttachment()) };
                yield return new object[] { new UpdateContactRequest(avatar: new FileAttachment()) };
                yield return new object[] { new CreateReplyRequest("bodyHtml", files: new[] { new FileAttachment() }) };
                yield return new object[] { new UpdateNoteRequest("bodyHtml", files: new[] { new FileAttachment() }) };
                yield return new object[] { new CreateTicketRequest(new TicketStatus(), new TicketPriority(), new TicketSource(), "description", email: "email", files: new[] { new FileAttachment() }) };
                yield return new object[] { new UpdateTicketRequest(files: new[] { new FileAttachment() }) };
            }

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }

        public class NoMultipartDataData : IEnumerable<object[]>
        {
            public IEnumerator<object[]> GetEnumerator()
            {
                yield return new object[] { new ContactCreateRequest("name", avatar: null) };
                yield return new object[] { new UpdateContactRequest(avatar: null) };
                yield return new object[] { new CreateReplyRequest("bodyHtml", files: new FileAttachment[] { }) };
                yield return new object[] { new CreateReplyRequest("bodyHtml", files: null) };
                yield return new object[] { new UpdateNoteRequest("bodyHtml", files: new FileAttachment[] { }) };
                yield return new object[] { new UpdateNoteRequest("bodyHtml", files: null) };
                yield return new object[] { new CreateTicketRequest(new TicketStatus(), new TicketPriority(), new TicketSource(), "description", email: "email", files: new FileAttachment[] { }) };
                yield return new object[] { new CreateTicketRequest(new TicketStatus(), new TicketPriority(), new TicketSource(), "description", email: "email", files: null) };
                yield return new object[] { new UpdateTicketRequest(files: new FileAttachment[] { }) };
                yield return new object[] { new UpdateTicketRequest(files: null) };
            }

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }
    }
}
