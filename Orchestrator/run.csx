/*
 * This function is not intended to be invoked directly. Instead it will be
 * triggered by an HTTP starter function.
 * 
 * Before running this sample, please:
 * - create a Durable activity function (default name is "Hello")
 * - create a Durable HTTP starter function
 */

#r "Microsoft.Azure.WebJobs.Extensions.DurableTask"

public static async Task<string> Run(DurableOrchestrationContext context, TraceWriter log)
{
    string input = context.GetInput<string>();
    string outputs = string.Empty;
    try
    {
        outputs = await context.CallActivityAsync<string>("Validator", input);
    
        if(outputs == "Success")
        {
            outputs = await context.CallActivityAsync<string>("Extractor", input);
        }
    }
    catch(Exception e)
    {
        log.Warning(e.ToString());
    }
    return outputs;
}