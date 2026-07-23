import type { MenuIconKey } from './iconMap';

export interface MenuTreeNode {
  key: string;
  title: string;
  parentKey: string | null;
  icon?: MenuIconKey;
  children?: MenuTreeNode[];
}

export type ContextMenuAction = 'add' | 'edit' | 'delete' | 'refresh';

export type MenuFormModalState =
  | { mode: 'add'; parentKey: string | null }
  | { mode: 'edit'; node: MenuTreeNode }
  | null;
