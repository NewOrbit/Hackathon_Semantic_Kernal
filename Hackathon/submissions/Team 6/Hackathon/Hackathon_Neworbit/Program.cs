using Hackathon_Neworbit;
using Microsoft.Extensions.Hosting;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel.Connectors.OpenAI;

var builder = Host.CreateEmptyApplicationBuilder(new HostApplicationBuilderSettings()
{
    Args = args
} );


var kernelBuilder = Kernel.CreateBuilder();
kernelBuilder.AddAzureOpenAIChatCompletion("test", "https://2024-09-11-openai.openai.azure.com/", "6b294e0dd8284dd38fafd4bf5e519ca0", modelId: "gpt-4o");
kernelBuilder.Plugins.AddFromType<LocationPlugin>();
kernelBuilder.Plugins.AddFromType<SpeechPlugin>();

var kernel = kernelBuilder.Build();


var chat = kernel.GetRequiredService<IChatCompletionService>();

OpenAIPromptExecutionSettings openAIPromptExecutionSettings = new OpenAIPromptExecutionSettings()
{
    ToolCallBehavior = ToolCallBehavior.AutoInvokeKernelFunctions,
};

var chatHistory = new ChatHistory();

chatHistory.AddSystemMessage(
    "You are a story telling AI." +
    "You will be telling a bed time story to children. " +
    "You will need to ask the parents how many children there." +
    "You should also ask the parents what the kids interests are." +
    "You should check for favourite locations and include only one of the locations but do not ask the user about this." +
    "The story should have some funny aspects to it. " +
    "Limit the story to 2 sentences." +
    "Read out loud");

while (true)
{
    var response = await chat.GetChatMessageContentsAsync(chatHistory, executionSettings: openAIPromptExecutionSettings, kernel: kernel);
    var lastMessage = response.Last();
    Console.WriteLine(lastMessage);

    var prompt = Console.ReadLine();
    chatHistory.AddUserMessage(prompt!);
}

