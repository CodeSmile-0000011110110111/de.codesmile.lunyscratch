#if UNREAL
using UnrealSharp.CoreUObject;
using UnrealSharp.Engine;

namespace LunyScratch;

public sealed class UnrealActions : IEngineActions
{
	public Double GetCurrentTimeInSeconds() => UGameplayStatics.TimeSeconds;
	public Double GetDeltaTimeInSeconds() => UGameplayStatics.WorldDeltaSeconds;


	public void Log(String message) => ShowMessage(message);
	public void ShowMessage(String message, Double duration = 2f) => AActor.PrintString(message, duration: (float)duration);
	public void PlaySound(String soundName, Double volume) => throw new NotImplementedException();

}
#endif
