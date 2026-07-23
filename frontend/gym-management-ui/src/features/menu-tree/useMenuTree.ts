import { App as AntdApp } from 'antd';
import { useCallback, useState } from 'react';
import { initialMenuData } from './mockMenuData';
import type { MenuTreeNode } from './types';

function cloneTree(nodes: MenuTreeNode[]): MenuTreeNode[] {
  return nodes.map((node) => ({
    ...node,
    children: node.children ? cloneTree(node.children) : undefined,
  }));
}

function insertNode(nodes: MenuTreeNode[], parentKey: string | null, node: MenuTreeNode): MenuTreeNode[] {
  if (parentKey === null) return [...nodes, node];
  return nodes.map((n) => {
    if (n.key === parentKey) return { ...n, children: [...(n.children ?? []), node] };
    if (n.children) return { ...n, children: insertNode(n.children, parentKey, node) };
    return n;
  });
}

function renameNode(nodes: MenuTreeNode[], key: string, title: string): MenuTreeNode[] {
  return nodes.map((n) => {
    if (n.key === key) return { ...n, title };
    if (n.children) return { ...n, children: renameNode(n.children, key, title) };
    return n;
  });
}

function removeNode(nodes: MenuTreeNode[], key: string): MenuTreeNode[] {
  return nodes
    .filter((n) => n.key !== key)
    .map((n) => (n.children ? { ...n, children: removeNode(n.children, key) } : n));
}

export function findNode(nodes: MenuTreeNode[], key: string): MenuTreeNode | null {
  for (const n of nodes) {
    if (n.key === key) return n;
    if (n.children) {
      const found = findNode(n.children, key);
      if (found) return found;
    }
  }
  return null;
}

export function useMenuTree() {
  const { message } = AntdApp.useApp();
  const [treeData, setTreeData] = useState<MenuTreeNode[]>(() => cloneTree(initialMenuData));
  const [loading, setLoading] = useState(false);

  const addNode = useCallback(
    (parentKey: string | null, title: string) => {
      // TODO: replace with POST /api/menu-tree once the backend endpoint exists.
      const node: MenuTreeNode = { key: `node-${Date.now()}`, title, parentKey };
      setTreeData((prev) => insertNode(prev, parentKey, node));
      void message.success(`Đã thêm "${title}"`);
    },
    [message],
  );

  const editNode = useCallback(
    (key: string, title: string) => {
      // TODO: replace with PUT /api/menu-tree/{key} once the backend endpoint exists.
      setTreeData((prev) => renameNode(prev, key, title));
      void message.success('Đã cập nhật mục menu');
    },
    [message],
  );

  const deleteNode = useCallback(
    (key: string) => {
      // TODO: replace with DELETE /api/menu-tree/{key} once the backend endpoint exists.
      setTreeData((prev) => removeNode(prev, key));
      void message.success('Đã xóa mục menu');
    },
    [message],
  );

  const refresh = useCallback(async () => {
    setLoading(true);
    try {
      // TODO: replace with GET /api/menu-tree once the backend endpoint exists.
      await new Promise((resolve) => setTimeout(resolve, 400));
      setTreeData(cloneTree(initialMenuData));
      void message.success('Đã làm mới menu');
    } finally {
      setLoading(false);
    }
  }, [message]);

  return { treeData, loading, addNode, editNode, deleteNode, refresh };
}
