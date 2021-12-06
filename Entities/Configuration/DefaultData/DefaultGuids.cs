using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Configuration
{
    public class DefaultGuids
    {
        protected static Guid CargoGuid 
        { 
            get { return new Guid("136fb0de-da57-4785-aa05-5fda69675661"); } 
        
        }

        protected static Guid TypeGuid
        {
            get { return new Guid("700e547e-d641-479d-af78-c07b8c666e3f"); }

        }

        protected static Guid CustomerGuid
        {
            get { return new Guid("679aec22-10e2-473e-ac91-7faeeb0f52ee"); }

        }

        protected static Guid OrderGuid
        {
            get { return new Guid("b079f7a0-32a0-426d-a86c-458a366901f3"); }

        }

        protected static Guid RouteGuid
        {
            get { return new Guid("6271c1ae-f4c2-46b0-bc94-629c54173ddb"); }

        }

        protected static Guid TrailerGuid
        {
            get { return new Guid("03811dd2-0b45-4127-ad15-5e73370a6a9a"); }

        }

        protected static Guid TruckGuid
        {
            get { return new Guid("9ac8905e-dd8e-43eb-b58d-9464da6fab6b"); }

        }

        protected static Guid LogistGuid
        {
            get { return new Guid("76c8ab55-452b-4657-b8ba-09aa76939ab4"); }

        }

        protected static Guid AdminGuid
        {
            get { return new Guid("421da925-6554-4e4b-a177-1e465795f36a"); }

        }
    }
}
