<<<<<<< HEAD
﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
=======
﻿using System.ComponentModel.DataAnnotations;
using CareerFIZ.Common;
using System;
>>>>>>> parent of 56d1541 (more updates)

namespace CareerFIZ.Models
{
    public partial class Payment
    {
<<<<<<< HEAD
        public int Id { get; set; }
        public decimal Amount { get; set; }
=======
        public int Id {  get; set; }

        public decimal Amount { get; set; }

>>>>>>> parent of 56d1541 (more updates)
        public DateTime PaymentDate { get; set; }
        public Guid AppUserId { get; set; }

        public virtual AppUser AppUser { get; set; } = null!;
    }
}
