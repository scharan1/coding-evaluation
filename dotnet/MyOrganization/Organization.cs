using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyOrganization
{
    internal abstract class Organization
    {
        private Position root;
        private int employeeId = 1;

        public Organization()
        {
            root = CreateOrganization();
        }

        protected abstract Position CreateOrganization();

        /**
         * hire the given person as an employee in the position that has that title
         * 
         * @param person
         * @param title
         * @return the newly filled position or empty if no position has that title
         */
        public Position? Hire(Name person, string title)
        {
            return HireHelper(root, person, title);
        }

        private Position? HireHelper(Position position, Name person, string title)
        {
            if (position.GetTitle().Equals(title) && !position.IsFilled())
            {
                position.SetEmployee(new Employee(employeeId++, person));
                return position;
            }
            else
            {
                foreach (var report in position.GetDirectReports())
                {
                    Position? found = HireHelper(report, person, title);
                    if (found != null)
                    {
                        return found;
                    }
                }
            }
            return null;
        }

        override public string ToString()
        {
            return PrintOrganization(root, "");
        }

        private string PrintOrganization(Position pos, string prefix)
        {
            StringBuilder sb = new StringBuilder(prefix + "+-" + pos.ToString() + "\n");
            foreach (Position p in pos.GetDirectReports())
            {
                sb.Append(PrintOrganization(p, prefix + "\t"));
            }
            return sb.ToString();
        }
    }
}
