using Application.DocuSign.DTOs;
using DocuSign.eSign.Model;
using System.IO;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IDocuSignService
    {
        Task<DocuSignDto> SendEnvelopeForEmbeddedSigning(string clientEmail,string clientName);
        Task<Envelope> GetEnvelope(string envelopeId);
        Task<Stream> GetEnvelopeFile(string envelopeId);
    }
}
