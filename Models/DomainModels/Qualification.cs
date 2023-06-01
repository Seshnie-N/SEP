﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SEP.Models.DomainModels
{
	public class Qualification
	{
		[Key]
		public int QualificationId { get; set; }
		[ForeignKey(nameof(Student))]
		public string StudentId { get; set; }
		public Student Student { get; set; }
        public string Institution { get; set; }
        public DateTime StartDate { get; set; }
		public DateTime EndDate { get; set;}
        public string Subjects { get; set; }
		public string Majors { get; set; }
        public string SubMajors { get; set; }
        public string Research { get; set; }
    }
}
