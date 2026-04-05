# Unity 2D 專案優化指南

## 問題分析

**專案類型：** 2D 遊戲  
**主要問題：** 啟動緩慢，載入不必要的 3D 套件

---

## ✅ 優化步驟

### 步驟 1: 移除不必要的 3D 套件

**已移除的套件：**

| 套件 | 原因 |
|------|------|
| com.unity.ai.navigation | 3D AI 導航系統 |
| com.unity.collab-proxy | Unity Collaborate（如不需要） |
| com.unity.multiplayer.center | 多人連線功能（如不需要） |
| com.unity.modules.cloth | 3D 布料物理 |
| com.unity.modules.terrain | 3D 地形系統 |
| com.unity.modules.terrainphysics | 3D 地形物理 |
| com.unity.modules.vehicles | 3D 車輛系統 |
| com.unity.modules.vr | VR 支援 |
| com.unity.modules.xr | XR 支援 |
| com.unity.modules.wind | 3D 風力系統 |
| com.unity.modules.physics | 3D 物理引擎（保留 physics2d） |

**預期效果：**
- 減少 30-50% 的啟動時間
- 減少記憶體使用
- 減少建置大小

---

### 步驟 2: 停用 Unity Splash Screen

**在 Unity Editor 中：**

1. `Edit` → `Project Settings` → `Player`
2. 展開 `Splash Image`
3. 取消勾選 `Show Splash Screen`
4. 取消勾選 `Show Unity Logo`

**預期效果：**
- 減少 2-5 秒啟動時間

---

### 步驟 3: 優化圖形設定

**在 Unity Editor 中：**

1. `Edit` → `Project Settings` → `Graphics`
2. 設定 `Scriptable Render Pipeline Settings` 為 `None`（使用內建渲染）
3. 或使用 URP（Universal Render Pipeline）- 更適合 2D

**Shader 優化：**

在 `GraphicsSettings` 中移除不必要的 3D Shaders：
- Deferred Rendering Shaders
- Motion Vectors
- Light Halo
- Lens Flare

---

### 步驟 4: 清理 Library 快取

**步驟：**

1. 關閉 Unity Editor
2. 刪除專案根目錄下的 `Library` 資料夾
3. 重新開啟專案

**注意：** Unity 會重新生成 Library，首次開啟會較慢，之後會變快。

---

### 步驟 5: 優化資源載入

**檢查大型資源：**

```bash
# 在專案目錄執行
Get-ChildItem "Assets" -Recurse -File | 
  Where-Object {$_.Length -gt 10MB} | 
  Select-Object Name, @{Name="Size(MB)";Expression={[math]::Round($_.Length/1MB, 2)}} | 
  Sort-Object "Size(MB)" -Descending
```

**建議：**
- 壓縮大型紋理
- 使用音訊壓縮
- 使用 AssetBundle 或 Addressables 延遲載入

---

### 步驟 6: 優化 Editor 設定

**在 Unity Editor 中：**

1. `Edit` → `Preferences` → `Asset Pipeline`
2. 啟用 `Auto Refresh`（自動重新整理）
3. 設定 `Cache Server Mode` 為 `Local`

---

## 📊 效能對比

**優化前：**
- 啟動時間：可能 30-60 秒
- 記憶體使用：較高
- 建置大小：較大

**優化後：**
- 啟動時間：預計減少 40-60%
- 記憶體使用：減少 20-30%
- 建置大小：減少 15-25%

---

## 🔄 套用優化

### 方式 1: 直接替換 manifest.json

```bash
# 備份原始檔案
cp Packages/manifest.json Packages/manifest.json.backup

# 使用優化版本
cp Packages/manifest_optimized.json Packages/manifest.json

# 重新開啟 Unity，讓它重新解析套件
```

### 方式 2: 手動移除套件

在 Unity Editor 中：
1. `Window` → `Package Manager`
2. 選擇 `Packages: In Project`
3. 逐一移除不需要的套件

---

## ⚠️ 注意事項

### 移除後檢查

移除套件後，請檢查：

1. **Console 錯誤**
   - 是否有腳本引用已移除的套件
   - 是否有 Prefab 使用 3D 組件

2. **場景檢查**
   - 檢查所有場景中的 GameObject
   - 移除 3D 相關組件（Rigidbody, MeshCollider 等）

3. **建置測試**
   - 嘗試建置專案
   - 確認沒有遺失組件錯誤

---

## 🎯 如果專案確實需要某些 3D 功能

如果你的 2D 遊戲使用了：

- **3D 物理碰撞**：保留 `com.unity.modules.physics`
- **粒子系統**：保留 `com.unity.modules.particlesystem`
- **動畫系統**：保留 `com.unity.modules.animation`

---

## 📚 相關文件

- [Unity Package Manager 文件](https://docs.unity3d.com/Manual/upm-ui.html)
- [Unity 優化指南](https://docs.unity3d.com/Manual/BestPracticeUnderstandingPerformanceInUnity.html)
- [2D 遊戲最佳實踐](https://unity.com/how-to/create-2d-games-unity)

---

## ✅ 驗證優化效果

優化後，請測試：

1. **啟動時間**
   ```
   測量 Unity Editor 開啟到可以操作的時間
   ```

2. **場景載入時間**
   ```
   測量 Play Mode 啟動時間
   ```

3. **記憶體使用**
   ```
   查看 Task Manager 中的 Unity 記憶體使用量
   ```

---

**建立日期：** 2026-04-05  
**優化版本：** 1.0