using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoManager
{
     class Video
    {
        public string Title { get; set; }
        public int Lenght { get; set; }

        protected Video(string title, int lenght)
        {
            Title = title;
            Lenght = lenght;
        }
    }
}
