# Unity MCP 完整指南

**專案：** Salmon  
**最後更新：** 2026-04-05  
**MCP 版本：** 0.63.3

---

## 📑 目錄

1. [測試報告](#測試報告)
2. [安裝指南](#安裝指南)
3. [設定指南](#設定指南)
4. [使用說明](#使用說明)
5. [工具列表](#工具列表)
6. [故障排除](#故障排除)

---

## ✅ 測試報告

### 測試時間

2026-04-05

### 測試結果

| 項目 | 狀態 | 說明 |
|------|------|------|
| MCP Plugin 安裝 | ✅ 成功 | 已正確安裝並載入 |
| 設定檔生成 | ✅ 成功 | AI-Game-Developer-Config.json 已生成 |
| Unity Editor | ✅ 運行中 | Salmon 專案已載入 |
| MCP 連線狀態 | ✅ Connected | AI Game Developer 視窗顯示 Connected |

### 功能測試

| 測試項目 | 狀態 | 結果 |
|---------|------|------|
| ✅ 連線狀態 | 成功 | Connected |
| ✅ 工具列表 | 成功 | 返回完整工具列表 |
| ✅ 場景資訊 | 成功 | Start 場景，7 個 Root GameObjects |
| ✅ 建立 GameObject | 成功 | TestCube 已建立 (instanceID: -8530) |
| ✅ 尋找 GameObject | 成功 | 功能正常 |
| ✅ Console 日誌 | 成功 | 返回 Console 記錄 |

### MCP 設定資訊

從設定檔 `UserSettings/AI-Game-Developer-Config.json` 中取得：

```json
{
  "host": "http://localhost:27361",
  "token": "kcsKY7BlxOb1O5RgRlNGcseHLlZPdqbJGAbf8ARY-Cc",
  "connectionMode": "Cloud",
  "transportMethod": "streamableHttp"
}
```

**關鍵資訊：**
- **URL**: `http://localhost:27361`
- **Port**: 27361
- **Token**: `kcsKY7BlxOb1O5RgRlNGcseHLlZPdqbJGAbf8ARY-Cc`
- **連線模式**: Cloud
- **傳輸方式**: streamableHttp

---

## 📦 安裝指南

### 前置需求

- Unity 6 或更新版本
- Node.js 18 或更新版本
- Git（可選，用於從 Git URL 安裝）

---

### 方法 1: 使用 .unitypackage 安裝（推薦）

#### 步驟 1: 下載 Plugin

**下載連結：**
```
https://github.com/IvanMurzak/Unity-MCP/releases
```

**下載選項：**
- ✅ `com.ivanmurzak.unity.mcp-0.63.3.zip`（推薦）
- ✅ `AI-Game-Dev-Installer.unitypackage`
- ✅ `Source code (zip)`

#### 步驟 2: 在 Unity 中安裝

**方式 A: 使用 .unitypackage（最簡單）**

1. 在 Unity Editor 中：`Assets` → `Import Package` → `Custom Package`
2. 選擇下載的 `.unitypackage` 檔案
3. Import 所有內容
4. 完成！

**方式 B: 解壓縮到 Packages 資料夾**

1. 解壓縮 `com.ivanmurzak.unity.mcp-0.63.3.zip`
2. 將資料夾重新命名為 `com.ivanmurzak.unity.mcp`
3. 移動到專案的 `Packages` 資料夾：
   ```
   D:\github\chiisen\Salmon\Packages\com.ivanmurzak.unity.mcp
   ```

#### 步驟 3: 更新 manifest.json

在 `Packages/manifest.json` 的 `dependencies` 中加入：

```json
{
  "dependencies": {
    "com.ivanmurzak.unity.mcp": "file:com.ivanmurzak.unity.mcp"
  }
}
```

---

### 方法 2: 使用 Git URL 安裝

#### 前置需求
- Git 必須正常運作
- 網路連線正常

#### 步驟

1. 在 Unity Editor 中：`Window` → `Package Manager`
2. 點擊 `+` → `Add package from git URL`
3. 輸入：
   ```
   https://github.com/IvanMurzak/Unity-MCP.git?path=/Unity-MCP-Plugin
   ```
4. 等待安裝完成

---

### 方法 3: 使用 CLI 安裝

```bash
# 安裝 unity-mcp-cli
npm install -g unity-mcp-cli

# 在專案目錄執行
unity-mcp-cli install-plugin .
```

---

### 方法 4: 從 OpenUPM 安裝

#### 前置需求
- 網路連線正常
- OpenUPM 可訪問

#### 步驟

在 `Packages/manifest.json` 中加入：

```json
{
  "dependencies": {
    "com.ivanmurzak.unity.mcp": "0.63.3"
  },
  "scopedRegistries": [
    {
      "name": "package.openupm.com",
      "url": "https://package.openupm.com",
      "scopes": [
        "com.ivanmurzak",
        "extensions.unity",
        "org.nuget.microsoft",
        "org.nuget.system"
      ]
    }
  ]
}
```

---

## ⚙️ 設定指南

### MCP 架構說明

```
AI 工具 (OpenCode/Claude/Cursor)
    ↓
MCP Server (Unity Plugin 內建)
    ↓
Unity Editor
```

---

### 步驟 1: 開啟 MCP 視窗

在 Unity Editor 中：
- `Window` → `AI Game Developer`

---

### 步驟 2: 檢查連線設定

在 AI Game Developer 視窗中：

**Connection Mode 選項：**
- **Local**: 使用本地端伺服器（預設）
- **Cloud**: 使用雲端伺服器
- **Custom**: 自訂伺服器 URL

**建議設定：**
- Connection Mode: **Local** 或 **Cloud**
- 如果選擇 Custom，URL: `http://localhost:8080`

---

### 步驟 3: 啟動 MCP Server

在 AI Game Developer 視窗中：
- 點擊 **Start Server** 或 **Connect**
- 等待狀態變為 **Connected**

---

### 步驟 4: 驗證連線

#### 方法 1: 在 Unity 中檢查

- MCP 視窗應顯示 "Connected"
- Server URL 應該顯示 `http://localhost:PORT`

#### 方法 2: 使用 CLI 檢查

```bash
unity-mcp-cli status .
```

#### 方法 3: 測試工具

```bash
# 列出所有工具
unity-mcp-cli run-tool tool-list . --url http://localhost:27361 --token "YOUR_TOKEN"

# 取得場景資訊
unity-mcp-cli run-tool scene-get-data . --url http://localhost:27361 --token "YOUR_TOKEN"
```

---

## 🎮 使用說明

### 在 OpenCode 中使用

你可以直接用自然語言操作 Unity：

#### 範例 1: 建立 GameObject

```
建立一個名為 "Player" 的 Sphere
```

#### 範例 2: 列出場景物件

```
列出當前 Unity 場景中的所有 GameObject
```

#### 範例 3: 搜尋資產

```
在 Unity 專案中搜尋所有 Material 資產
```

#### 範例 4: 取得場景資訊

```
顯示當前場景的詳細資訊
```

---

### 使用 CLI 工具

#### 基本命令

```bash
# 檢查狀態
unity-mcp-cli status .

# 開啟專案
unity-mcp-cli open .

# 等待就緒
unity-mcp-cli wait-for-ready .

# 列出工具
unity-mcp-cli run-tool tool-list . --url http://localhost:27361 --token "YOUR_TOKEN"
```

#### 場景操作

```bash
# 取得場景資訊
unity-mcp-cli run-tool scene-get-data . --url http://localhost:27361 --token "YOUR_TOKEN"

# 開啟場景
unity-mcp-cli run-tool scene-open . --url http://localhost:27361 --input '{"scenePath":"Assets/Scenes/MyScene.unity"}'

# 儲存場景
unity-mcp-cli run-tool scene-save . --url http://localhost:27361
```

#### GameObject 操作

```bash
# 建立 GameObject
unity-mcp-cli run-tool gameobject-create . --url http://localhost:27361 --input '{"name":"TestCube"}'

# 尋找 GameObject
unity-mcp-cli run-tool gameobject-find . --url http://localhost:27361 --input '{"query":"Camera"}'

# 刪除 GameObject
unity-mcp-cli run-tool gameobject-destroy . --url http://localhost:27361 --input '{"instanceID":-1234}'
```

#### 組件操作

```bash
# 加入組件
unity-mcp-cli run-tool gameobject-component-add . --url http://localhost:27361 --input '{"instanceID":-1234,"componentType":"Rigidbody"}'

# 取得組件
unity-mcp-cli run-tool gameobject-component-get . --url http://localhost:27361 --input '{"instanceID":-1234,"componentType":"Rigidbody"}'

# 修改組件
unity-mcp-cli run-tool gameobject-component-modify . --url http://localhost:27361 --input '{"instanceID":-1234,"componentType":"Rigidbody","properties":{"mass":10}}'
```

---

## 🛠️ 工具列表

### 場景操作

| 工具名稱 | 功能 | 狀態 |
|---------|------|------|
| scene-get-data | 取得場景資訊 | ✅ 已啟用 |
| scene-create | 建立新場景 | ✅ 已啟用 |
| scene-open | 開啟場景 | ✅ 已啟用 |
| scene-save | 儲存場景 | ✅ 已啟用 |
| scene-set-active | 設定作用中場景 | ✅ 已啟用 |
| scene-unload | 卸載場景 | ✅ 已啟用 |
| scene-list-opened | 列出已開啟場景 | ✅ 已啟用 |

### GameObject 操作

| 工具名稱 | 功能 | 狀態 |
|---------|------|------|
| gameobject-create | 建立 GameObject | ✅ 已啟用 |
| gameobject-find | 尋找 GameObject | ✅ 已啟用 |
| gameobject-modify | 修改 GameObject | ✅ 已啟用 |
| gameobject-destroy | 刪除 GameObject | ✅ 已啟用 |
| gameobject-duplicate | 複製 GameObject | ✅ 已啟用 |
| gameobject-set-parent | 設定父物件 | ✅ 已啟用 |

### 組件操作

| 工具名稱 | 功能 | 狀態 |
|---------|------|------|
| gameobject-component-add | 加入組件 | ✅ 已啟用 |
| gameobject-component-get | 取得組件 | ✅ 已啟用 |
| gameobject-component-modify | 修改組件 | ✅ 已啟用 |
| gameobject-component-destroy | 刪除組件 | ✅ 已啟用 |
| gameobject-component-list-all | 列出所有組件 | ✅ 已啟用 |

### 資產管理

| 工具名稱 | 功能 | 狀態 |
|---------|------|------|
| assets-find | 尋找資產 | ✅ 已啟用 |
| assets-get-data | 取得資產資訊 | ✅ 已啟用 |
| assets-modify | 修改資產 | ✅ 已啟用 |
| assets-material-create | 建立 Material | ✅ 已啟用 |
| assets-prefab-create | 建立 Prefab | ✅ 已啟用 |
| assets-prefab-instantiate | 實例化 Prefab | ✅ 已啟用 |
| assets-refresh | 重新整理資產 | ✅ 已啟用 |

### 除錯工具

| 工具名稱 | 功能 | 狀態 |
|---------|------|------|
| console-get-logs | 取得 Console 日誌 | ✅ 已啟用 |
| console-clear-logs | 清除 Console | ❌ 已停用 |
| tool-list | 列出所有工具 | ✅ 已啟用 |
| tests-run | 執行測試 | ✅ 已啟用 |

### 腳本操作

| 工具名稱 | 功能 | 狀態 |
|---------|------|------|
| script-execute | 執行腳本 | ✅ 已啟用 |
| script-read | 讀取腳本 | ❌ 已停用 |
| script-update-or-create | 建立/更新腳本 | ❌ 已停用 |
| script-delete | 刪除腳本 | ❌ 已停用 |

---

## 🔧 故障排除

### 問題 1: 無法連接到 MCP Server

**症狀：**
- MCP 視窗顯示 "Disconnected"
- CLI 無法連線

**解決方案：**

1. **檢查 Unity Editor 是否已開啟專案**
   ```bash
   unity-mcp-cli status .
   ```

2. **重新啟動 MCP Server**
   - 在 Unity: `Window` → `AI Game Developer`
   - 點擊 **Stop Server**
   - 再點擊 **Start Server**

3. **檢查 Port 是否被占用**
   ```bash
   netstat -ano | findstr "27361"
   ```

4. **使用 CLI 重新開啟**
   ```bash
   unity-mcp-cli open .
   unity-mcp-cli wait-for-ready .
   ```

---

### 問題 2: Plugin 安裝失敗

**症狀：**
- Unity Console 顯示錯誤
- manifest.json 格式錯誤

**解決方案：**

1. **檢查 manifest.json 格式**
   ```bash
   Get-Content "Packages\manifest.json" | ConvertFrom-Json
   ```

2. **手動修復 manifest.json**
   - 確保 JSON 格式正確
   - 確保沒有語法錯誤

3. **使用 .unitypackage 方式安裝**
   - 下載 `.unitypackage` 檔案
   - 在 Unity: `Assets` → `Import Package` → `Custom Package`

---

### 問題 3: Git 執行失敗

**症狀：**
- 錯誤碼 3221225794
- Git fetch 失敗

**解決方案：**

1. **檢查 Git 是否正常**
   ```bash
   git --version
   ```

2. **重新安裝 Git**
   - 下載官方版本：https://git-scm.com/download/win
   - 移除 Scoop 版本：`scoop uninstall git`
   - 安裝官方版本

3. **使用手動安裝方式**
   - 下載 `.unitypackage` 或 ZIP 檔案
   - 手動安裝（參考安裝指南）

---

### 問題 4: OpenUPM 無法連線

**症狀：**
- ENOTFOUND 錯誤
- DNS 解析失敗

**解決方案：**

1. **檢查網路連線**
   ```bash
   Test-NetConnection package.openupm.com -Port 443
   ```

2. **使用 VPN**
   - 開啟 VPN 後重試

3. **使用手動安裝方式**
   - 下載 `.unitypackage` 或 ZIP 檔案
   - 手動安裝（參考安裝指南）

---

### 問題 5: MCP 工具無法執行

**症狀：**
- 工具執行失敗
- 返回錯誤訊息

**解決方案：**

1. **檢查工具是否已啟用**
   - 在 Unity: `Window` → `AI Game Developer`
   - 檢查 Tools 列表
   - 確認工具狀態為 Enabled

2. **檢查參數格式**
   - 確認 JSON 格式正確
   - 確認所有必需參數都已提供

3. **查看 Console 日誌**
   ```bash
   unity-mcp-cli run-tool console-get-logs . --url http://localhost:27361 --token "YOUR_TOKEN"
   ```

---

## 📚 相關文件

### 官方文件

- [Unity MCP GitHub](https://github.com/IvanMurzak/Unity-MCP)
- [CLI 文件](https://github.com/IvanMurzak/Unity-MCP/blob/main/cli/README.md)
- [工具列表](https://github.com/IvanMurzak/Unity-MCP/blob/main/docs/default-mcp-tools.md)
- [Releases](https://github.com/IvanMurzak/Unity-MCP/releases)

### MCP 協議

- [Model Context Protocol](https://modelcontextprotocol.io/)
- [MCP Specification](https://spec.modelcontextprotocol.io/)

---

## 🎉 完成！

Unity MCP 已完全設定並正常運作！

**你現在可以：**
- ✅ 在 OpenCode 中用自然語言操作 Unity
- ✅ 使用 CLI 工具控制 Unity
- ✅ 使用所有 MCP 工具
- ✅ AI 輔助開發 Unity 專案

---

**最後更新：** 2026-04-05  
**版本：** 1.0