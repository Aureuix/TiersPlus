/*
public class TPAudioClip
{
    private static AudioClip LoadAudioClipInternal(string file)
    {
        if (file.IndexOf('.') == -1) file += ".wav";
        string modName = "Tiers+";
        if (cachedAudioClips.ContainsKey(modName + ":" + file))
        {
            return cachedAudioClips[modName + ":" + file];
        }
        string filePath = Path.Combine("Assets", file);
        if (mod?.HasModFile(filePath) ?? false)
        {
            GadgetModFile modFile = mod.GetModFile(filePath);
            using (WWW www = new WWW("file:///" + modFile.FilePath))
            {
                AudioClip clip = null;
                Stopwatch s = new Stopwatch();
                s.Start();
                try
                {
                    for (int i = 0; !www.isDone && s.ElapsedMilliseconds < 1000; i++) Thread.Sleep(5);
                    clip = www.GetAudioClip(true, true);
                    cachedAudioClips.Add(modName + ":" + file, clip);
                    return clip;
                }
                finally
                {
                    s.Stop();
                    if (clip != null)
                    {
                        modFile.DisposeOnCondition(() => clip?.loadState != AudioDataLoadState.Loading);
                    }
                    else
                    {
                        modFile.Dispose();
                    }
                }
            }
        }
        else
        {
            return null;
        }
    }
}*/