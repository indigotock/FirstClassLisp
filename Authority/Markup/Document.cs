using System;
using System.Collections.Generic;
using System.Text;

namespace Authority.Markup
{
    public class Document : IElement
    {
        public DocumentMetadata Metadata;

        public IElement Body;

        public class DocumentMetadata
        {

            string Title = "No title";
            Author[] Authors = new Author[] { new Author() { } };

            public class Author
            {
                public string[] Names = new String[] { "No Author(s)" };
                public string Name
                {
                    get => string.Join(" ", Names);
                    set => Names = new string[] { value };
                }
            }
        }
    }
}
