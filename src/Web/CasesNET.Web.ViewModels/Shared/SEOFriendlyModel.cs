namespace CasesNET.Web.ViewModels.Shared
{
    using CasesNET.Common;

    public abstract class SEOFriendlyModel
    {
        public string Url => $@"/{this.FriendlyUrlPrefix}/{this.FriendlyUrlName.ToSEOFriendlyURL()}";

        public virtual string FriendlyUrlPrefix { get; set; }

        public virtual string FriendlyUrlName { get; set; }
    }
}
