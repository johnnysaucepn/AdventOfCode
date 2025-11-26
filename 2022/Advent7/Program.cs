using System.Text.RegularExpressions;

Part1();
Part2();

void Part1()
{
    Console.WriteLine("----Part 1----");
    var lines = File.ReadAllLines("input.txt").ToList();
    var root = BuildDirectoryStructure(lines);
    DumpTree(root);
    List<Tuple<string, int>> candidateList = new List<Tuple<string, int>>();
    Traverse(root, candidateList);

    var total = candidateList.Where(x=>x.Item2 < 100000).Select(x=>x.Item2).Sum();
    Console.WriteLine(total);
}

void Part2()
{
    Console.WriteLine("----Part 2----");
    var lines = File.ReadAllLines("input.txt").ToList();
    var root = BuildDirectoryStructure(lines);
    List<(string, int)> bigDirs = new List<(string,int)>();

    var totalUsed = root.GetSize();
    var totalSpace = 70000000;
    var requiredSpace = 30000000;
    var currentFree = totalSpace - totalUsed;
    var spaceToFree = requiredSpace - currentFree;

    List<Tuple<string, int>> candidateList = new List<Tuple<string, int>>();
    Traverse(root, candidateList);
 
    var candidatesBySize = candidateList.Where(x=>x.Item2 > spaceToFree).OrderBy(d=>d.Item2);
    foreach (var candidate in candidatesBySize) Console.WriteLine($"Candidate {candidate.Item1} {candidate.Item2}");

    var smallest = candidatesBySize.First();
  
    Console.WriteLine($"Choose {smallest.Item1} {smallest.Item2}");
}

void DumpTree(AdventDir current, int depth = 0)
{
    var padding = new string(Enumerable.Repeat(' ', depth*2).ToArray());
    Console.WriteLine($"{padding}- {current.Name} (dir)");
    foreach (var dir in current.Dirs.Values)
    {
        DumpTree(dir, depth + 1);
    }
    foreach (var file in current.Files)
    {
        Console.WriteLine($"{padding}  - {file.Name} (file, size={file.OwnSize})");
    }
}

void Traverse(AdventDir current, List<Tuple<string, int>> dirList)
{
    // Should be able to create an enumerable/yield sequence, but couldn't get the syntax right!
    dirList.Add(new Tuple<string, int>(current.Name, current.GetSize()));

    foreach (var dir in current.Dirs.Values)
    {
        Traverse(dir, dirList);
    }
}

AdventDir BuildDirectoryStructure(List<string> lines)
{
    var cdRegex = new Regex(@"^\$ cd (.*)$");
    var ls = "$ ls";
    var fileRegex = new Regex(@"^([0-9]+) (.*)$");
    var dirRegex = new Regex(@"^dir (.*)$");
    
    var root = new AdventDir("/", null);
    AdventDir currentDir = root;

    foreach (var line in lines)
    {
        if (line == ls) continue;
        var cdMatch = cdRegex.Match(line);
        if (cdMatch.Success)
        {
            var dirName = cdMatch.Groups[1].Value;
            if (dirName == "/")
            {
                currentDir = root;
            }
            else if (dirName == "..")
            {
                currentDir = currentDir.Parent;
            }
            else if (currentDir.Dirs.ContainsKey(dirName))
            {
                currentDir = currentDir.Dirs[dirName];
            }
            else
            {
                throw new Exception($"Directory {dirName} not found - current dir is {currentDir.Name}");
            }
            continue;
        }
        var dirMatch = dirRegex.Match(line);
        if (dirMatch.Success)
        {
            var dirName = dirMatch.Groups[1].Value;
            var newDir = new AdventDir(dirName, currentDir);
            currentDir.Dirs[dirName] = newDir;
            continue;
        }
        var fileMatch = fileRegex.Match(line);
        if (fileMatch.Success)
        {
            var size = Convert.ToInt32(fileMatch.Groups[1].Value);
            var fileName = fileMatch.Groups[2].Value;
            var newFile = new AdventFile(fileName, size);
            currentDir.Files.Add(newFile);
            continue;
        }
        throw new FormatException($"Syntax error - {line}");
        
    }

    return root;
}


class AdventDir
{
    public string Name;

    public AdventDir Parent;
    public Dictionary<string, AdventDir> Dirs = new Dictionary<string, AdventDir>();
    public List<AdventFile> Files = new List<AdventFile>();

    public AdventDir(string name, AdventDir parent)
    {
        Name = name;
        Parent = parent;
    }

    public int GetSize()
    {
        var fileTotal = Files.Sum(f=>f.OwnSize);
        var dirTotal = Dirs.Values.Sum(d=>d.GetSize());

        return fileTotal + dirTotal;
    }
}

class AdventFile
{
    public string Name;
    public int OwnSize;

    public AdventFile(string name, int size)
    {
        Name = name;
        OwnSize = size;
    }
}
