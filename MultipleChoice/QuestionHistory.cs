using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultipleChoice
{
    [Serializable]
    public class QuestionHistory
    {
        public string QuestionNumber { get; set; }
        public int TimesAnswered { get; set; }
        public int TimesAnsweredCorrect { get; set; }

        public double GetAnsweredCorrectPercentage()
        {
            if (TimesAnswered == 0)
            {
                return 0.0d;
            }
            return Math.Round((double)TimesAnsweredCorrect / TimesAnswered * 100, 2);
        }
    }
}
