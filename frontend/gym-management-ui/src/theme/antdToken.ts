/**
 * FitTrace → Ant Design theme map.
 *
 * Usage:
 *   import { ConfigProvider, theme as antdAlgo } from 'antd';
 *   import { fitTraceTheme } from './tokens/antdToken';
 *
 *   const base = fitTraceTheme('dark');
 *   const theme = { ...base, algorithm: antdAlgo[base.algorithm as 'darkAlgorithm'] };
 *   <ConfigProvider theme={theme}>…</ConfigProvider>
 *
 * Every value is derived from tokens/token.ts — never hard-code hexes in components.
 */

/**
 * NOTE: antd is intentionally NOT imported here — this file is copied into an antd
 * codebase where the package resolves. `algorithm` is returned as a string key; wire it
 * to `theme.darkAlgorithm` / `theme.defaultAlgorithm` at the ConfigProvider (see usage).
 */
import {
  palette, semantic, typography, spacing, radius, shadow, motion, type ThemeMode,
} from './token';

/** Structural stand-in for antd's ThemeConfig (avoids importing the package). */
type ThemeConfig = {
  algorithm?: unknown;
  token?: Record<string, unknown>;
  components?: Record<string, Record<string, unknown>>;
  cssVar?: { key: string };
  hashed?: boolean;
};

/** Build the antd `token` (global) block from FitTrace semantics for a given mode. */
function globalToken(mode: ThemeMode): ThemeConfig['token'] {
  const s = semantic[mode];
  const sh = shadow[mode];
  return {
    // ---- Brand / semantic colors ----
    colorPrimary: s.accent,
    colorInfo: s.info,
    colorSuccess: s.success,
    colorWarning: s.warning,
    colorError: s.danger,
    colorLink: s.accent,
    colorLinkHover: s.accentHover,
    colorLinkActive: s.accentActive,

    // ---- Neutral surfaces & text ----
    colorBgLayout: s.bg,
    colorBgContainer: s.surface,
    colorBgElevated: s.surface2,
    colorBgSpotlight: s.surface3,
    colorBgMask: s.overlay,
    colorBorder: s.borderStrong,
    colorBorderSecondary: s.border,
    colorSplit: s.divider,
    colorText: s.text,
    colorTextSecondary: s.textMuted,
    colorTextTertiary: s.textSubtle,
    colorTextQuaternary: s.textSubtle,

    // ---- Typography ----
    fontFamily: typography.fontSans,
    fontFamilyCode: typography.fontMono,
    fontSize: typography.size.md,
    fontSizeSM: typography.size.sm,
    fontSizeLG: typography.size.lg,
    fontSizeHeading1: typography.size['4xl'],
    fontSizeHeading2: typography.size['2xl'],
    fontSizeHeading3: typography.size.xl,
    fontSizeHeading4: typography.size.lg,
    fontSizeHeading5: typography.size.md,
    lineHeight: typography.leading.normal,
    fontWeightStrong: typography.weight.bold,

    // ---- Shape ----
    borderRadius: radius.md,
    borderRadiusXS: radius.xs,
    borderRadiusSM: radius.sm,
    borderRadiusLG: radius.lg,
    borderRadiusOuter: radius.md,

    // ---- Spacing / sizing (4px grid) ----
    sizeUnit: 4,
    sizeStep: 4,
    padding: spacing[4],
    paddingXS: spacing[2],
    paddingSM: spacing[3],
    paddingLG: spacing[6],
    margin: spacing[4],
    marginXS: spacing[2],
    marginLG: spacing[6],
    controlHeight: 40,
    controlHeightSM: 32,
    controlHeightLG: 48,

    // ---- Elevation ----
    boxShadow: sh.md,
    boxShadowSecondary: sh.sm,
    boxShadowTertiary: sh.xs,

    // ---- Motion ----
    motionDurationFast: `${motion.duration.fast}ms`,
    motionDurationMid: `${motion.duration.base}ms`,
    motionDurationSlow: `${motion.duration.slow}ms`,
    motionEaseInOut: motion.ease.standard,
    motionEaseOut: motion.ease.out,

    // ---- Focus ring ----
    colorPrimaryBorder: s.accent,
    controlOutline: s.accentRing,
    controlOutlineWidth: 3,
    wireframe: false,
  };
}

/** Per-component overrides that CSS-token parity can't express globally. */
function components(mode: ThemeMode): ThemeConfig['components'] {
  const s = semantic[mode];
  const sh = shadow[mode];
  return {
    Button: {
      primaryColor: s.accentFg,
      primaryShadow: sh.glowAccent,
      defaultBg: s.surface2,
      defaultColor: s.text,
      defaultBorderColor: s.borderStrong,
      fontWeight: typography.weight.semibold,
      controlHeight: 40,
      borderRadius: radius.md,
    },
    Input: {
      colorBgContainer: s.surfaceInset,
      activeBorderColor: s.accent,
      hoverBorderColor: s.borderStrong,
      activeShadow: `0 0 0 3px ${s.accentSoft}`,
      borderRadius: radius.md,
      controlHeight: 42,
    },
    Select: {
      colorBgContainer: s.surfaceInset,
      optionSelectedBg: s.accentSoft,
      optionSelectedColor: s.accent,
      borderRadius: radius.md,
      controlHeight: 42,
    },
    Card: {
      colorBgContainer: s.surface,
      colorBorderSecondary: s.border,
      borderRadiusLG: radius.lg,
      paddingLG: spacing[6],
    },
    Modal: {
      contentBg: s.surface,
      headerBg: s.surface,
      borderRadiusLG: radius.xl,
      titleColor: s.text,
      titleFontSize: typography.size.xl,
    },
    Tabs: {
      inkBarColor: s.accent,
      itemActiveColor: s.text,
      itemSelectedColor: s.text,
      itemHoverColor: s.text,
      itemColor: s.textMuted,
      titleFontSize: typography.size.md,
    },
    Switch: {
      handleBg: s.accentFg,
      colorPrimary: s.accent,
      colorPrimaryHover: s.accentHover,
    },
    Progress: {
      defaultColor: s.accent,
      remainingColor: s.surface3,
    },
    Tag: {
      defaultBg: s.surface2,
      defaultColor: s.text,
      borderRadiusSM: radius.sm,
    },
    Tooltip: {
      colorBgSpotlight: palette.slate950,
      colorTextLightSolid: palette.slate50,
      borderRadius: radius.sm,
    },
    Table: {
      headerBg: s.bgSubtle,
      headerColor: s.textSubtle,
      rowHoverBg: s.surface2,
      colorBgContainer: s.surface,
      borderColor: s.divider,
    },
    Menu: {
      itemSelectedBg: s.accentSoft,
      itemSelectedColor: s.accent,
      itemHoverBg: s.surface2,
      itemColor: s.textMuted,
      itemHeight: 42,
      borderRadius: radius.md,
    },
  };
}

/** Full antd ThemeConfig for a FitTrace mode. Defaults to dark (the product default). */
export function fitTraceTheme(mode: ThemeMode = 'dark'): ThemeConfig {
  return {
    // string key — map to theme.darkAlgorithm / theme.defaultAlgorithm at the ConfigProvider
    algorithm: mode === 'dark' ? 'darkAlgorithm' : 'defaultAlgorithm',
    token: globalToken(mode),
    components: components(mode),
    cssVar: { key: 'fittrace' },
    hashed: true,
  };
}

export const fitTraceDark = fitTraceTheme('dark');
export const fitTraceLight = fitTraceTheme('light');

export default fitTraceTheme;
