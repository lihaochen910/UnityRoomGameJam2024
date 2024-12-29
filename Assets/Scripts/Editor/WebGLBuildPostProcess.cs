using UnityEditor;
using UnityEditor.Callbacks;
using System.IO;
using System.Linq;
using UnityEngine;


namespace GameJam.Editor {

	public class WebGLBuildPostProcess
	{
		[PostProcessBuild(1)]
		public static void OnPostprocessBuild(BuildTarget target, string pathToBuiltProject)
		{
			// 只在WebGL构建时执行
			if (target != BuildTarget.WebGL)
				return;
			
			// Debug.Log( $"pathToBuiltProject: {pathToBuiltProject}" );

			// 获取构建输出目录中的所有文件
			var files = Directory.GetFiles(pathToBuiltProject, "*.*", SearchOption.AllDirectories)
								 .Where(file => 
									 file.EndsWith(".data") ||
									 file.EndsWith(".data.unityweb") ||
									 file.EndsWith(".framework.js") ||
									 file.EndsWith(".framework.js.unityweb") ||
									 file.EndsWith(".wasm") ||
									 file.EndsWith(".wasm.unityweb"));

			// 为每个文件添加.gz后缀
			foreach (var file in files)
			{
				string newFileName;
				if (file.EndsWith(".unityweb"))
				{
					// 移除.unityweb后缀并添加.gz
					newFileName = file.Substring(0, file.Length - ".unityweb".Length) + ".gz";
				}
				else
				{
					// 直接添加.gz后缀
					newFileName = file + ".gz";
				}

				if ( File.Exists( newFileName ) ) {
					File.Delete( newFileName );
				}

				try
				{
					File.Move(file, newFileName);
					Debug.Log($"已重命名: {Path.GetFileName(file)} -> {Path.GetFileName(newFileName)}");
				}
				catch (System.Exception e)
				{
					Debug.LogError($"重命名文件失败: {Path.GetFileName(file)}\n错误: {e.Message}");
				}
			}
        
			Debug.Log("WebGL构建后处理完成：已添加.gz后缀");
		}
	}

}
