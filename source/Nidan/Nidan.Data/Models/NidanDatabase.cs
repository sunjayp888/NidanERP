


namespace Nidan.Data.Models
{
    using System.Data.Entity;
    using Entity;

    public partial class NidanDatabase : OrganisationDbContext
    {
        public NidanDatabase() : base("name=NidanDatabase")
        {
        }

        public virtual DbSet<AbsenceType> AbsenceTypes { get; set; }
        public virtual DbSet<Alert> Alerts { get; set; }
        public virtual DbSet<AspNetUsersAlertSchedule> AspNetUsersAlertSchedules { get; set; }
        public virtual DbSet<Colour> Colours { get; set; }
        public virtual DbSet<Host> Hosts { get; set; }
        public virtual DbSet<Organisation> Organisations { get; set; }
        public virtual DbSet<Personnel> Personnels { get; set; }
        public virtual DbSet<UserAuthorisationFilter> UserAuthorisationFilters { get; set; }
        public virtual DbSet<EventBudget> EventBudgets { get; set; }
        public virtual DbSet<Enquiry> Enquiries { get; set; }
        public virtual DbSet<Centre> Centres { get; set; }
        public virtual DbSet<Event> Events { get; set; }
        public virtual DbSet<Mobilization> Mobilizations { get; set; }

        public virtual DbSet<EventActivityType> EventActivityTypes { get; set; }
        public virtual DbSet<Question> Questions { get; set; }
        public virtual DbSet<EventQuestion> EventQuestions { get; set; }
        public virtual DbSet<AreaOfInterest> AreaOfInterests { get; set; }
        public virtual DbSet<CasteCategory> CasteCategories { get; set; }
        public virtual DbSet<FollowUp> FollowUps { get; set; }
        public virtual DbSet<Course> Courses { get; set; }
        public virtual DbSet<HowDidYouKnowAbout> HowDidYouKnowAbouts { get; set; }
        public virtual DbSet<Qualification> Qualifications { get; set; }
        public virtual DbSet<Religion> Religions { get; set; }
        public virtual DbSet<Occupation> Occupations { get; set; }
        public virtual DbSet<EnquirySearchField> EnquirySearchFields { get; set; }
        public virtual DbSet<MobilizationType> MobilizationTypes { get; set; }
        public virtual DbSet<Scheme> Schemes { get; set; }
        public virtual DbSet<SchemeType> SchemeTypes { get; set; }
        public virtual DbSet<BatchTimePrefer> BatchTimePrefers { get; set; }
        public virtual DbSet<Sector> Sectors { get; set; }
        public virtual DbSet<EnquiryType> EnquiryTypes { get; set; }
        public virtual DbSet<StudentType> StudentTypes { get; set; }
        public virtual DbSet<Counselling> Counsellings { get; set; }
        public virtual DbSet<Batch> Batches { get; set; }
        public virtual DbSet<Admission> Admissions { get; set; }
        public virtual DbSet<District> Districts { get; set; }
        public virtual DbSet<State> States { get; set; }
        public virtual DbSet<Taluka> Talukas { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
           

          
            modelBuilder.Entity<Alert>()
                .HasMany(e => e.AspNetUsersAlertSchedules)
                .WithRequired(e => e.Alert)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Colour>()
                .Property(e => e.Hex)
                .IsUnicode(false);

            modelBuilder.Entity<Course>()
               .Property(e => e.Name)
               .IsUnicode(false);

            modelBuilder.Entity<Course>()
                .HasMany(e => e.FollowUps)
                .WithRequired(e => e.Course)
                .HasForeignKey(e => e.IntrestedCourseId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Course>()
               .HasMany(e => e.Counsellings)
               .WithRequired(e => e.Course)
               .HasForeignKey(e => e.CourseOfferedId)
               .WillCascadeOnDelete(false);

            modelBuilder.Entity<FollowUp>()
                .Property(e => e.Name)
                .IsUnicode(false);


            //modelBuilder.Entity<Colour>()
            //    .HasMany(e => e.Divisions)
            //    .WithRequired(e => e.Colour)
            //    .WillCascadeOnDelete(false);

            //modelBuilder.Entity<Company>()
            //    .HasMany(e => e.Divisions)
            //    .WithRequired(e => e.Company)
            //    .WillCascadeOnDelete(false);

            //modelBuilder.Entity<Country>()
            //    .HasMany(e => e.CountryAbsenceTypes)
            //    .WithRequired(e => e.Country)
            //    .WillCascadeOnDelete(false);

            //modelBuilder.Entity<Country>()
            //    .HasMany(e => e.CountryPublicHolidays)
            //    .WithRequired(e => e.Country)
            //    .WillCascadeOnDelete(false);

            //modelBuilder.Entity<Country>()
            //    .HasMany(e => e.DivisionCountryAbsencePeriods)
            //    .WithRequired(e => e.Country)
            //    .WillCascadeOnDelete(false);

            //modelBuilder.Entity<Country>()
            //    .HasMany(e => e.DivisionCountryWorkingPatterns)
            //    .WithRequired(e => e.Country)
            //    .WillCascadeOnDelete(false);

            //modelBuilder.Entity<Country>()
            //    .HasMany(e => e.Personnels)
            //    .WithRequired(e => e.Country)
            //    .WillCascadeOnDelete(false);

            //modelBuilder.Entity<Country>()
            //    .HasMany(e => e.Sites)
            //    .WithRequired(e => e.Country)
            //    .WillCascadeOnDelete(false);

            //modelBuilder.Entity<CountryAbsenceType>()
            //    .HasMany(e => e.DivisionCountryAbsenceTypeEntitlements)
            //    .WithRequired(e => e.CountryAbsenceType)
            //    .WillCascadeOnDelete(false);


            //modelBuilder.Entity<Division>()
            //    .HasMany(e => e.DivisionCountryAbsencePeriods)
            //    .WithRequired(e => e.Division)
            //    .WillCascadeOnDelete(false);

            //modelBuilder.Entity<Division>()
            //    .HasMany(e => e.DivisionCountryAbsenceTypeEntitlements)
            //    .WithRequired(e => e.Division)
            //    .WillCascadeOnDelete(false);

            //modelBuilder.Entity<Division>()
            //    .HasMany(e => e.DivisionCountryWorkingPatterns)
            //    .WithRequired(e => e.Division)
            //    .WillCascadeOnDelete(false);

            //modelBuilder.Entity<Division>()
            //    .HasMany(e => e.DivisionSites)
            //    .WithRequired(e => e.Division)
            //    .WillCascadeOnDelete(false);

            //modelBuilder.Entity<Division>()
            //    .HasMany(e => e.Employments)
            //    .WithRequired(e => e.Division)
            //    .WillCascadeOnDelete(false);

            //modelBuilder.Entity<DivisionCountryAbsencePeriod>()
            //    .HasMany(e => e.PersonnelAbsenceEntitlements)
            //    .WithRequired(e => e.DivisionCountryAbsencePeriod)
            //    .WillCascadeOnDelete(false);



            //modelBuilder.Entity<Frequency>()
            //    .HasMany(e => e.DivisionCountryAbsenceTypeEntitlements)
            //    .WithRequired(e => e.Frequency)
            //    .WillCascadeOnDelete(false);



            modelBuilder.Entity<Organisation>()
                .HasMany(e => e.Alerts)
                .WithRequired(e => e.Organisation)
                .WillCascadeOnDelete(false);

        
            //modelBuilder.Entity<Organisation>()
            //    .HasMany(e => e.CountryAbsenceTypes)
            //    .WithRequired(e => e.Organisation)
            //    .WillCascadeOnDelete(false);

            //modelBuilder.Entity<Organisation>()
            //    .HasMany(e => e.CountryPublicHolidays)
            //    .WithRequired(e => e.Organisation)
            //    .WillCascadeOnDelete(false);


            //modelBuilder.Entity<Organisation>()
            //    .HasMany(e => e.Divisions)
            //    .WithRequired(e => e.Organisation)
            //    .WillCascadeOnDelete(false);

            //modelBuilder.Entity<Organisation>()
            //    .HasMany(e => e.DivisionCountryAbsencePeriods)
            //    .WithRequired(e => e.Organisation)
            //    .WillCascadeOnDelete(false);

            //modelBuilder.Entity<Organisation>()
            //    .HasMany(e => e.DivisionCountryAbsenceTypeEntitlements)
            //    .WithRequired(e => e.Organisation)
            //    .WillCascadeOnDelete(false);

            //modelBuilder.Entity<Organisation>()
            //    .HasMany(e => e.DivisionCountryWorkingPatterns)
            //    .WithRequired(e => e.Organisation)
            //    .WillCascadeOnDelete(false);

            //modelBuilder.Entity<Organisation>()
            //    .HasMany(e => e.DivisionSites)
            //    .WithRequired(e => e.Organisation)
            //    .WillCascadeOnDelete(false);

            modelBuilder.Entity<Organisation>()
                .HasMany(e => e.Hosts)
                .WithRequired(e => e.Organisation)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Organisation>()
                .HasMany(e => e.Personnels)
                .WithRequired(e => e.Organisation)
                .WillCascadeOnDelete(false);

          

            modelBuilder.Entity<Personnel>()
                .Property(e => e.Telephone)
                .IsUnicode(false);

            modelBuilder.Entity<Personnel>()
                .Property(e => e.Mobile)
                .IsUnicode(false);

            modelBuilder.Entity<Personnel>()
                .Property(e => e.NINumber)
                .IsUnicode(false);

            modelBuilder.Entity<Personnel>()
                .Property(e => e.BankAccountNumber)
                .IsUnicode(false);

            modelBuilder.Entity<Personnel>()
                .Property(e => e.BankSortCode)
                .IsUnicode(false);

            modelBuilder.Entity<Personnel>()
                .Property(e => e.BankTelephone)
                .IsUnicode(false);



            //modelBuilder.Entity<PublicHoliday>()
            //    .HasMany(e => e.CountryPublicHolidays)
            //    .WithRequired(e => e.PublicHoliday)
            //    .WillCascadeOnDelete(false);


            //modelBuilder.Entity<Site>()
            //    .HasMany(e => e.DivisionSites)
            //    .WithRequired(e => e.Site)
            //    .WillCascadeOnDelete(false);

            //modelBuilder.Entity<WorkingPattern>()
            //    .HasMany(e => e.DivisionCountryWorkingPatterns)
            //    .WithRequired(e => e.WorkingPattern)
            //    .WillCascadeOnDelete(false);



            //modelBuilder.Entity<CompanyBuilding>()
            //.HasMany(e => e.Buildings)
            //.WithRequired(e => e.CompanyBuilding)
            //.WillCascadeOnDelete(false);

            modelBuilder.Entity<Event>()
              .Property(e => e.Name)
              .IsUnicode(false);
            modelBuilder.Entity<Enquiry>()
                .Property(e => e.CandidateName)
                .IsUnicode(false);

            modelBuilder.Entity<Event>()
                .Property(e => e.CreatedBy);

            modelBuilder.Entity<Enquiry>()
                .Property(e => e.EmailId)
                .IsUnicode(false);

            modelBuilder.Entity<EventActivityType>()
                .Property(e => e.Name)
                .IsFixedLength();

            //modelBuilder.Entity<Enquiry>()
            //    .Property(e => e.QualificationId);

            modelBuilder.Entity<Centre>()
                  .Property(e => e.CentreCode)
                  .IsUnicode(false);

            modelBuilder.Entity<Centre>()
                .Property(e => e.Name)
                .IsUnicode(false);

            //modelBuilder.Entity<Centre>()
            //    .HasMany(e => e.Events)
            //    .WithRequired(e => e.Centre)
            //    .WillCascadeOnDelete(false);

            modelBuilder.Entity<Event>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Event>()
                .Property(e => e.CreatedBy)
                .IsUnicode(false);

            modelBuilder.Entity<Enquiry>()
                .Property(e => e.Address)
                .IsUnicode(false);

            modelBuilder.Entity<Enquiry>()
                .Property(e => e.GuardianName)
                .IsUnicode(false);

            modelBuilder.Entity<Enquiry>()
                .Property(e => e.OccupationId);


            modelBuilder.Entity<Enquiry>()
                .Property(e => e.ReligionId);

            modelBuilder.Entity<Enquiry>()
                .Property(e => e.CasteCategoryId);

            modelBuilder.Entity<Enquiry>()
                .Property(e => e.Gender)
                .IsUnicode(false);

            modelBuilder.Entity<Enquiry>()
                .Property(e => e.EducationalQualificationId);

            modelBuilder.Entity<Enquiry>()
                .Property(e => e.YearOfPassOut)
                .IsUnicode(false);

            modelBuilder.Entity<Enquiry>()
                .Property(e => e.Marks)
                .IsUnicode(false);

            modelBuilder.Entity<Course>()
              .Property(e => e.Name)
              .IsUnicode(false);

            modelBuilder.Entity<Course>()
                .HasMany(e => e.Enquiries)
                .WithRequired(e => e.Course)
                .HasForeignKey(e => e.IntrestedCourseId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Course>()
                .HasMany(e => e.Mobilizations)
                .WithRequired(e => e.Course)
                .HasForeignKey(e => e.InterestedCourseId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Enquiry>()
                .Property(e => e.CandidateName)
                .IsUnicode(false);

            modelBuilder.Entity<Enquiry>()
                .Property(e => e.EmailId)
                .IsUnicode(false);

            //modelBuilder.Entity<Enquiry>()
            //    .Property(e => e.QualificationId)
            //    .IsUnicode(false);

            modelBuilder.Entity<Enquiry>()
                .Property(e => e.Address)
                .IsUnicode(false);

            modelBuilder.Entity<Enquiry>()
                .Property(e => e.GuardianName)
                .IsUnicode(false);

            //modelBuilder.Entity<Enquiry>()
            //    .Property(e => e.OccupationId)
            //    .IsUnicode(false);

            //modelBuilder.Entity<Enquiry>()
            //    .Property(e => e.Religion)
            //    .IsUnicode(false);

            //modelBuilder.Entity<Enquiry>()
            //    .Property(e => e.CategoryCode)
            //    .IsUnicode(false);

            modelBuilder.Entity<AreaOfInterest>()
                    .Property(e => e.Name)
                    .IsUnicode(false);

            modelBuilder.Entity<CasteCategory>()
                .Property(e => e.Caste)
                .IsUnicode(false);

            modelBuilder.Entity<CasteCategory>()
                .HasMany(e => e.Enquiries)
                .WithRequired(e => e.CasteCategory)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Centre>()
                .Property(e => e.CentreCode)
                .IsUnicode(false);

            modelBuilder.Entity<Centre>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Centre>()
                .HasMany(e => e.Enquiries)
                .WithRequired(e => e.Centre)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Colour>()
                .Property(e => e.Hex)
                .IsUnicode(false);

            modelBuilder.Entity<Enquiry>()
                .Property(e => e.CandidateName)
                .IsUnicode(false);

            modelBuilder.Entity<Enquiry>()
                .Property(e => e.EmailId)
                .IsUnicode(false);

            modelBuilder.Entity<Enquiry>()
                .Property(e => e.Address)
                .IsUnicode(false);

            modelBuilder.Entity<Enquiry>()
                .Property(e => e.GuardianName)
                .IsUnicode(false);

            modelBuilder.Entity<Enquiry>()
                .Property(e => e.Gender)
                .IsUnicode(false);

            modelBuilder.Entity<Enquiry>()
                .Property(e => e.YearOfPassOut)
                .IsUnicode(false);

            modelBuilder.Entity<Enquiry>()
                .Property(e => e.Marks)
                .IsUnicode(false);

            modelBuilder.Entity<Enquiry>()
                .Property(e => e.PreTrainingStatus)
                .IsUnicode(false);

            modelBuilder.Entity<Enquiry>()
                .Property(e => e.EmploymentStatus)
                .IsUnicode(false);

            modelBuilder.Entity<Enquiry>()
                .Property(e => e.Promotional)
                .IsUnicode(false);

            modelBuilder.Entity<Enquiry>()
                .Property(e => e.Place)
                .IsUnicode(false);

            modelBuilder.Entity<Enquiry>()
                .Property(e => e.EnquiryStatus)
                .IsUnicode(false);

            //modelBuilder.Entity<Enquiry>()
            //    .HasOptional(e => e.Enquiry1)
            //    .WithRequired(e => e.Enquiry2);

            modelBuilder.Entity<FollowUp>()
                .Property(e => e.Name)
                .IsUnicode(false);

            //modelBuilder.Entity<FollowUp>()
            //    .HasOptional(e => e.FollowUp1)
            //    .WithRequired(e => e.FollowUp2);

            modelBuilder.Entity<HowDidYouKnowAbout>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<HowDidYouKnowAbout>()
                .HasMany(e => e.Enquiries)
                .WithRequired(e => e.HowDidYouKnowAbout)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Mobilization>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Mobilization>()
                .Property(e => e.Remark)
                .IsUnicode(false);

            modelBuilder.Entity<Mobilization>()
                .Property(e => e.MobilizerStatus)
                .IsUnicode(false);

           

            modelBuilder.Entity<Mobilization>()
                .Property(e => e.StudentLocation)
                .IsUnicode(false);

            modelBuilder.Entity<Mobilization>()
                .Property(e => e.OtherInterestedCourse)
                .IsUnicode(false);

            //modelBuilder.Entity<Mobilization>()
            //    .HasOptional(e => e.Mobilization1)
            //    .WithRequired(e => e.Mobilization2);

            modelBuilder.Entity<Occupation>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Occupation>()
                .HasMany(e => e.Enquiries)
                .WithRequired(e => e.Occupation)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Qualification>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Qualification>()
                .HasMany(e => e.Enquiries)
                .WithRequired(e => e.Qualification)
                .HasForeignKey(e => e.EducationalQualificationId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Religion>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Religion>()
                .HasMany(e => e.Enquiries)
                .WithRequired(e => e.Religion)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Course>()
               .Property(e => e.Name)
               .IsUnicode(false);

            modelBuilder.Entity<EnquirySearchField>()
                .Property(e => e.CandidateName)
                .IsUnicode(false);

            modelBuilder.Entity<EnquirySearchField>()
                .Property(e => e.EmailId)
                .IsUnicode(false);

            modelBuilder.Entity<EnquirySearchField>()
                .Property(e => e.Address)
                .IsUnicode(false);

            modelBuilder.Entity<EnquirySearchField>()
                .Property(e => e.GuardianName)
                .IsUnicode(false);

            modelBuilder.Entity<EnquirySearchField>()
                .Property(e => e.Gender)
                .IsUnicode(false);

            modelBuilder.Entity<EnquirySearchField>()
                .Property(e => e.YearOFPassOut)
                .IsUnicode(false);

            modelBuilder.Entity<EnquirySearchField>()
                .Property(e => e.Marks)
                .IsUnicode(false);

            modelBuilder.Entity<EnquirySearchField>()
                .Property(e => e.PreTrainingStatus)
                .IsUnicode(false);

            modelBuilder.Entity<EnquirySearchField>()
                .Property(e => e.EmploymentStatus)
                .IsUnicode(false);

            modelBuilder.Entity<EnquirySearchField>()
                .Property(e => e.Promotional)
                .IsUnicode(false);

            modelBuilder.Entity<EnquirySearchField>()
                .Property(e => e.Place)
                .IsUnicode(false);

            modelBuilder.Entity<EnquirySearchField>()
                .Property(e => e.CounselledBy)
                .IsUnicode(false);

            modelBuilder.Entity<EnquirySearchField>()
                .Property(e => e.Remarks)
                .IsUnicode(false);

            modelBuilder.Entity<EnquirySearchField>()
                .Property(e => e.EnquiryStatus)
                .IsUnicode(false);

            modelBuilder.Entity<EnquirySearchField>()
                .Property(e => e.SearchField)
                .IsUnicode(false);

            modelBuilder.Entity<MobilizationType>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Scheme>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<SchemeType>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<SchemeType>()
                .HasMany(e => e.Schemes)
                .WithRequired(e => e.SchemeType)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<BatchTimePrefer>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Sector>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<EnquiryType>()
               .Property(e => e.Name)
               .IsUnicode(false);

            modelBuilder.Entity<StudentType>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Counselling>()
                 .Property(e => e.CounselledBy)
                 .IsUnicode(false);

            modelBuilder.Entity<Counselling>()
                .Property(e => e.PreferTiming)
                .IsUnicode(false);

            modelBuilder.Entity<Counselling>()
                .Property(e => e.Remarks)
                .IsUnicode(false);

            modelBuilder.Entity<Counselling>()
                .Property(e => e.RemarkByBranchManager)
                .IsUnicode(false);

            
            modelBuilder.Entity<Admission>()
                .Property(e => e.Salutation)
                .IsUnicode(false);

            modelBuilder.Entity<Admission>()
                .Property(e => e.FirstName)
                .IsUnicode(false);

            modelBuilder.Entity<Admission>()
                .Property(e => e.MiddleName)
                .IsUnicode(false);

            modelBuilder.Entity<Admission>()
                .Property(e => e.LastName)
                .IsUnicode(false);

            modelBuilder.Entity<Admission>()
                .Property(e => e.EmailId)
                .IsUnicode(false);

            modelBuilder.Entity<Admission>()
                .Property(e => e.Gender)
                .IsUnicode(false);

            modelBuilder.Entity<Admission>()
                .Property(e => e.FatherName)
                .IsUnicode(false);

            modelBuilder.Entity<Admission>()
                .Property(e => e.PermanentAddress)
                .IsUnicode(false);

            modelBuilder.Entity<Admission>()
                .Property(e => e.PTalukaId);

            modelBuilder.Entity<Admission>()
                .Property(e => e.PDistrictId);

            modelBuilder.Entity<Admission>()
                .Property(e => e.PStateId);

            modelBuilder.Entity<Admission>()
                .Property(e => e.CommunicationAddress)
                .IsUnicode(false);

            modelBuilder.Entity<Admission>()
                .Property(e => e.CTalukaId);

            modelBuilder.Entity<Admission>()
                .Property(e => e.CDistrictId);

            modelBuilder.Entity<Admission>()
                .Property(e => e.CStateId);

            modelBuilder.Entity<Admission>()
                .Property(e => e.ProfessionalQualification)
                .IsUnicode(false);

            modelBuilder.Entity<Admission>()
                .Property(e => e.TechnicalQualification)
                .IsUnicode(false);

            modelBuilder.Entity<Admission>()
                .Property(e => e.PreTrainingStatus)
                .IsUnicode(false);

            modelBuilder.Entity<Admission>()
                .Property(e => e.EmploymentStatus)
                .IsUnicode(false);

            modelBuilder.Entity<Admission>()
                .Property(e => e.EmployerName)
                .IsUnicode(false);

            modelBuilder.Entity<Admission>()
                .Property(e => e.EmployerAddress)
                .IsUnicode(false);

            modelBuilder.Entity<District>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<District>()
                .HasMany(e => e.Talukas)
                .WithRequired(e => e.District)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<State>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<State>()
                .HasMany(e => e.Districts)
                .WithRequired(e => e.State)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<State>()
                .HasMany(e => e.Talukas)
                .WithRequired(e => e.State)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Taluka>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Batch>()
                .Property(e => e.TrainingType)
                .IsUnicode(false);

            modelBuilder.Entity<Batch>()
                .Property(e => e.SubSector)
                .IsUnicode(false);

            modelBuilder.Entity<Batch>()
                .Property(e => e.Remarks)
                .IsUnicode(false);

            base.OnModelCreating(modelBuilder);
        }
    }
}
