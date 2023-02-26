public static TourOptions Options(bool isAuthenticated)
{
    var tourOptions = new TourOptions();

    if (!isAuthenticated)
    {
        tourOptions.Steps.Add(new TourOptions.Step
        {
            Id = "example-step",
            Text = "I'm from C#!!",
            AttachTo = new AttachTo
            {
                Element = "#addAction"
            }
        });

        tourOptions.Steps.Add(new TourOptions.Step
        {
            Id = "example-step",
            Text = "second",
            AttachTo = new AttachTo
            {
                Element = "#addAction"
            }
        });
    }

    return tourOptions;
}