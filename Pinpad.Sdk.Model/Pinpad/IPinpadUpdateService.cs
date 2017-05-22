namespace Pinpad.Sdk.Model.Pinpad
{
    // TODO: Doc & impl
    public interface IPinpadUpdateService
    {
        int SectionSize { get; }
        byte [] NextPackageSection { get; }

        bool Load (string filePath);
        bool Update ();
    }
}
