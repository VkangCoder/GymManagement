import { useMemo, useState, type ReactNode } from 'react';
import { App as AntdApp, ConfigProvider, theme as antdAlgo, type ThemeConfig } from 'antd';
import { fitTraceTheme } from './antdToken';
import { semantic, type ThemeMode } from './token';
import { ThemeModeContext, type ThemeModeContextValue } from './themeModeContext';

const algorithmByKey = {
  darkAlgorithm: antdAlgo.darkAlgorithm,
  defaultAlgorithm: antdAlgo.defaultAlgorithm,
} as const;

interface ThemeProviderProps {
  children: ReactNode;
}

export default function ThemeProvider({ children }: ThemeProviderProps) {
  const [mode, setMode] = useState<ThemeMode>('dark');

  const theme = useMemo<ThemeConfig>(() => {
    const base = fitTraceTheme(mode);
    return {
      ...base,
      algorithm: algorithmByKey[base.algorithm as keyof typeof algorithmByKey],
    } as ThemeConfig;
  }, [mode]);

  const contextValue = useMemo<ThemeModeContextValue>(
    () => ({
      mode,
      toggleMode: () => setMode((prev) => (prev === 'dark' ? 'light' : 'dark')),
    }),
    [mode],
  );

  return (
    <ThemeModeContext.Provider value={contextValue}>
      <ConfigProvider theme={theme}>
        <AntdApp style={{ height: '100%', background: semantic[mode].bg }}>{children}</AntdApp>
      </ConfigProvider>
    </ThemeModeContext.Provider>
  );
}
