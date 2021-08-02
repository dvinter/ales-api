using System;
using System.Collections.Generic;

namespace agendaFacilAPI.Classes
{
    public class DatasDisponiveis
    {
        public DatasDisponiveis()
        {
        }

        public List<DateTime> data { get; set; }
        public List<string> hora { get; set; }
    }
}