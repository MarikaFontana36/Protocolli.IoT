using CoAPNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Protocolli.IoT.Fontana_Scapolan.Worker
{
    public abstract class CoapResource
    {
        public Uri Uri => Metadata.UriReference;

        public CoapResourceMetadata Metadata { get; set; }

        public CoapResource(string uri)
            : this(new Uri(uri, UriKind.Relative)) { }

        public CoapResource(Uri uri)
        {
            Metadata = new CoapResourceMetadata(uri);
        }

        public CoapResource(CoapResourceMetadata metadata)
        {
            Metadata = metadata;
        }

        public virtual Task<CoapMessage> GetAsync(CoapMessage request, ICoapConnectionInformation connectionInformation)
            => GetAsync(request);

        public virtual Task<CoapMessage> GetAsync(CoapMessage request)
            => Task.FromResult(Get(request));

        public virtual CoapMessage Get(CoapMessage request)
        {
            return new CoapMessage
            {
                Code = CoapMessageCode.MethodNotAllowed,
                Token = request.Token
            };
        }

        public virtual Task<CoapMessage> PutAsync(CoapMessage request, ICoapConnectionInformation connectionInformation)
            => PutAsync(request);

        public virtual Task<CoapMessage> PutAsync(CoapMessage request)
            => Task.FromResult(Put(request));

        public virtual CoapMessage Put(CoapMessage request)
        {
            return new CoapMessage
            {
                Code = CoapMessageCode.MethodNotAllowed,
                Token = request.Token
            };
        }

        public virtual Task<CoapMessage> PostAsync(CoapMessage request, ICoapConnectionInformation connectionInformation)
            => PostAsync(request);

        public virtual Task<CoapMessage> PostAsync(CoapMessage request)
            => Task.FromResult(Post(request));

        public virtual CoapMessage Post(CoapMessage request)
        {
            return new CoapMessage
            {
                Code = CoapMessageCode.MethodNotAllowed,
                Token = request.Token
            };
        }

        public virtual Task<CoapMessage> DeleteAsync(CoapMessage request, ICoapConnectionInformation connectionInformation)
            => DeleteAsync(request);

        public virtual Task<CoapMessage> DeleteAsync(CoapMessage request)
            => Task.FromResult(Delete(request));

        public virtual CoapMessage Delete(CoapMessage request)
        {
            return new CoapMessage
            {
                Code = CoapMessageCode.MethodNotAllowed,
                Token = request.Token
            };
        }
    }
}
