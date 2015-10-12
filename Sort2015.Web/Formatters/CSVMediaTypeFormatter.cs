using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Sort2015.Web.Formatters
{
    public class CSVMediaTypeFormatter : MediaTypeFormatter
    {
        private string filename = String.Empty;
        public CSVMediaTypeFormatter()
        {

            SupportedMediaTypes.Add(new System.Net.Http.Headers.MediaTypeHeaderValue("text/csv"));
            MediaTypeMappings.Add(new QueryStringMapping("format", "csv", "text/csv"));
            // New code:
            SupportedEncodings.Add(new UTF8Encoding(encoderShouldEmitUTF8Identifier: false));
            SupportedEncodings.Add(Encoding.GetEncoding("iso-8859-1"));
        }

        public override bool CanReadType(Type type)
        {
            return true;
        }

        public override bool CanWriteType(Type type)
        {
            if (type == null)
                throw new ArgumentNullException("type");

            return isTypeOfIEnumerable(type);
        }

        private bool isTypeOfIEnumerable(Type type)
        {
            foreach (Type interfaceType in type.GetInterfaces())
            {
                try
                {
                    if (interfaceType.GetGenericTypeDefinition() == typeof(IEnumerable<>))
                    {
                        filename = type.GetGenericArguments()[0].Name + "_" + DateTime.Now.ToString("yyyyMMdd-hhmmss") + ".csv";
                        return true;
                    }
                }
                catch (Exception e)
                {
                    var errMessage = e.Message;
                }

            }
            return false;
        }

        public override Task WriteToStreamAsync(Type type, object value, Stream stream, HttpContent content, TransportContext transportContext)
        {
            return WriteStream(type, value, stream, content);
        }

        public override void SetDefaultContentHeaders(
            Type type, HttpContentHeaders headers, MediaTypeHeaderValue mediaType)
        {
            base.SetDefaultContentHeaders(type, headers, mediaType);
            headers.Add("Content-Disposition", "attachment; filename=" + filename);
        }

        private Task WriteStream(Type type, object value, Stream stream, HttpContent content)
        {
            //NOTE: We have check the type inside CanWriteType method
            //If request comes this far, the type is IEnumerable. We are safe.
            StreamWriter writer = new StreamWriter(stream);

            WriteHeader(type, writer);
            var items = value as IEnumerable<object>;
            foreach (var item in items)
            {
                WriteItem(item, writer);
            }

            return writer.FlushAsync();
        }
        private void WriteHeader(Type type, StreamWriter writer)
        {
            Type itemType = type.GetGenericArguments()[0];

            var headerLine = string.Join<string>(
                ",", itemType.GetProperties().Select(x => x.Name)
            );

            writer.WriteLine(headerLine);
        }
        private void WriteItem(object item, StreamWriter writer)
        {
            string _valueLine = string.Empty;

            var vals = item.GetType().GetProperties().Select(
                   pi => new
                   {
                       Value = pi.GetValue(item, null)
                   }
               );

            foreach (var val in vals)
            {
                string _val = string.Empty;
                if (val.Value != null)
                {
                    _val = Escape(val.Value.ToString());
                }
                _valueLine = string.Concat(_valueLine, _val, ",");
            }
            writer.WriteLine(_valueLine.TrimEnd(','));
        }

        static char[] _specialChars = new char[] { ',', '\n', '\r', '"' };
        private string Escape(object o)
        {
            if (o == null)
            {
                return "";
            }
            string field = o.ToString();
            if (field.IndexOfAny(_specialChars) != -1)
            {
                // Delimit the entire field with quotes and replace embedded quotes with "".
                return String.Format("\"{0}\"", field.Replace("\"", "\"\""));
            }
            else return field;
        }
    }
}
