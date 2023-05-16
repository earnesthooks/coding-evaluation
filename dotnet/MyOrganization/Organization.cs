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
            //your code here
            // Find Position
            Position position = FindThePosition(root, title);

            if (position != null)
            {
                //Set Identifier
                var identifier = position.GetHashCode();
                //Instantiate New Employee
                position.SetEmployee(new Employee(identifier, person));
                return position;
            }

            return null;
        }

        //Need to Add a Method to find position hashcode/identifier
        private Position FindThePosition(Position position, string title)
        {
            if (position.GetTitle() == title)
            {
                return position;
            }

            foreach (Position directReport in position.GetDirectReports())
            {
                Position foundPosition = FindThePosition(directReport, title);
                if (foundPosition != null)
                {
                    return foundPosition;
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
