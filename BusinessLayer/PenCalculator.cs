using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;
using WebApplication1.DataLayer;
using WebApplication1.Models;

namespace WebApplication1.BusinessLayer
{
    public class PenCalculator:iPenCalculator
    {
        public readonly iSQL _SQL;
        public PenCalculator(iSQL SQLDataHelper)
        {
            this._SQL = SQLDataHelper;
        }

        public int GetBusinessDays(Country Country, input input)
        {
            int days = 0;
            DateTime startDate = input.startDate;
            DateTime returnDate = input.returnDate;

            for (var date = startDate; date<=returnDate; date=date.AddDays(1))
            {
                bool checkWeekend = false;
                bool checkHoliday = false;

                for (int i = 0; i <Country.Weekend.Count; i++)
                {
                    if (date.DayOfWeek.ToString() == (Country.Weekend[i].WeekendDay))
                    {
                        checkWeekend = true;
                    }
                }
                for (int i=0; i<Country.Holidays.Count; i++)
                {
                    if (date.DayOfYear == (Country.Holidays[i].Holiday).DayOfYear)
                    {
                        checkHoliday = true;
                    }
                }
                if (checkWeekend != true && checkHoliday != true)
                {
                    days++;
                }
            }
            return days;
        }


        public double CalculatePenalty(Country country, input input)
        {
            int workDays = GetBusinessDays(country, input);
            double penalty;
            if (workDays > 10)
            {
                penalty = (workDays - 10) * (50 * country.countryExchange);
            }
            else
            {
                penalty = 0;
            }
            return penalty;
        }

        public outputGiven ShowPenalty(input input)
        {
            
            
            double penalty = CalculatePenalty(input.Country, input);
            outputGiven output = new outputGiven(penalty, input.Country.currencySymbol);
            return output;
        }

        public List<Country> ShowCountries()
        {
            List<Country> countryList = _SQL.GetCountries();
            return countryList;
        }

    }
}
